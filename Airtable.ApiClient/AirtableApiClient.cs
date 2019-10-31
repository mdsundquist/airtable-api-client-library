using Airtable.ApiClient.Attributes;
using Airtable.ApiClient.Entities;
using Airtable.ApiClient.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Airtable.ApiClient
{
    /// <summary>
    ///     A .NET Standard 2.1 client library for making Airtable API (v0) calls using C# objects
    /// </summary>
    public class AirtableApiClient
    {
        private readonly IAirtableHttpClient httpClient;

        /// <summary>
        ///     Constructor assumes the Airtable API key and database ("base") id 
        ///     are already configured by the HttpClientFactory prior to injection
        /// </summary>
        /// <param name="httpClient"></param>
        public AirtableApiClient(IAirtableHttpClient httpClient) =>
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

        #region ======= GET methods =======
        [return: MaybeNull]
        public async Task<AirtableRecord> Retrieve(string table, string recordId) => 
            await this.Retrieve(table, recordId, null)!.ConfigureAwait(false);

        [return: MaybeNull]
        public async Task<AirtableRecord> Retrieve(string table, string id, 
            IEnumerable<(AirtableFieldValuePoco ObjectType, string[] FieldNames)>? conversionPairs)
        {
            if (String.IsNullOrEmpty(table)) throw new ArgumentNullException(nameof(table));
            if (String.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            AirtableRecord resultRecord =
                (await BuildAirTableApiRequest<AirtableRecord>(HttpVerb.GET, $"{table}/{id}").ConfigureAwait(false)).Single();

            return ConvertAirtableFieldValuesToPocos(resultRecord, conversionPairs)!;
        }

        public async Task<List<AirtableRecord>> List(string table,
                                                     string[]? fields = null,
                                                     string? filterFormula = null,
                                                     int? maxRecords = null,
                                                     int? pageSize = null,
                                                     dynamic[]? sort = null,
                                                     string? view = null,
                                                     string? cellFormat = null,
                                                     string? timeZone = null,
                                                     string? userLocale = null,
                                                     string? offset = null,
                                                     IEnumerable<(AirtableFieldValuePoco ObjectType, string[] FieldNames)>? conversionPairs = null)
        {
            if (String.IsNullOrEmpty(table)) throw new ArgumentNullException(nameof(table), "table parameter cannot be null or empty");

            var requestUri = new UriBuilder
            {
                Path = table,
                Query = BuildListQueryUriParameters(fields, filterFormula, maxRecords, pageSize, sort, view, cellFormat,
                                                 timeZone, userLocale, offset)
            };

            var recordList = new List<AirtableRecord>();
            do
            {
                AirtableListResponse listResponse = (await BuildAirTableApiRequest<AirtableListResponse>(
                    HttpVerb.GET, requestUri.Uri.PathAndQuery).ConfigureAwait(false)).Single();

                List<AirtableRecord>? recordsReceived = listResponse.Records;

                if (recordsReceived?.Any() == true)
                {
                    if (conversionPairs?.Any() == true)
                        recordsReceived = ConvertAirtableFieldValuesToPocos(recordsReceived, conversionPairs);

                    recordList.AddRange(recordsReceived);
                }
                offset = listResponse.Offset;

            } while (!String.IsNullOrEmpty(offset));

            return recordList;
        }
        #endregion ======= GET methods =======

        #region ======= POST methods =======
        public async Task<AirtableRecord> Create(string table, AirtableFieldsDictionary fields) =>
            (await Create(table, new List<AirtableRecord> { new AirtableRecord { Fields = fields } })
                .ConfigureAwait(false)).SingleOrDefault();

        public async Task<AirtableRecord> Create(string table, AirtableRecord record) =>
            (await Create(table, new List<AirtableRecord> { record }).ConfigureAwait(false)).SingleOrDefault();

        public async Task<List<AirtableRecord>> Create(string table, IEnumerable<AirtableFieldsDictionary> fieldSets) =>
            await Create(table, fieldSets.Select(f => new AirtableRecord { Fields = f })).ConfigureAwait(false);

        public async Task<List<AirtableRecord>> Create(string table, IEnumerable<AirtableRecord> records)
        {
            if (String.IsNullOrWhiteSpace(table)) throw new ArgumentNullException(nameof(table), "table is null or empty");
            if (records?.Any() != true) throw new ArgumentNullException(nameof(records), "records is null or empty");
            if (records.Where(r => r.Fields?.Any() != true) == null) throw new ArgumentNullException(nameof(records), "one or more record.Fields is null or empty");

            return await BuildAirTableApiRequest<AirtableRecord>(HttpVerb.POST, table, records).ConfigureAwait(false);
        }
        #endregion ======= POST methods =======

        #region ======= PUT methods =======
        public async Task<AirtableRecord> Put(string table, AirtableRecord record) =>
            (await Put(table, new AirtableRecord[] { record }).ConfigureAwait(false)).Single();

        public async Task<List<AirtableRecord>> Put(string table, IEnumerable<AirtableRecord> records)
        {
            if (String.IsNullOrWhiteSpace(table)) throw new ArgumentNullException(nameof(table));
            if (records?.Any() != true) throw new ArgumentNullException(nameof(records));

            return await BuildAirTableApiRequest<AirtableRecord>(HttpVerb.PUT, table, records).ConfigureAwait(false);
        }
        #endregion ======= PUT methods =======

        #region ======= PATCH methods =======
        [return: MaybeNull]
        public async Task<AirtableRecord> Patch(string table, AirtableRecord newRecord, AirtableRecord oldRecord)
        {
            if (newRecord is null)
                throw new ArgumentNullException(nameof(newRecord));

            if (newRecord.Equals(oldRecord)) return null!;

            var records = new List<(AirtableRecord NewRecord, AirtableRecord OldRecord)> { (newRecord, oldRecord) };

            List<AirtableRecord> result = await Patch(table, records).ConfigureAwait(false);

            return result.SingleOrDefault();
        }

        public async Task<List<AirtableRecord>> Patch(string table, IEnumerable<(AirtableRecord NewRecord, AirtableRecord OldRecord)> recordPairs)
        {
            if (String.IsNullOrEmpty(table)) throw new ArgumentNullException(nameof(table));
            if (recordPairs?.Any() != true) throw new ArgumentNullException(nameof(recordPairs));
            if (recordPairs.Any(r => String.IsNullOrWhiteSpace(r.NewRecord.Id) || String.IsNullOrWhiteSpace(r.OldRecord.Id)))
                throw new ArgumentNullException("one or more records contains a null or empty Id property");
            if (recordPairs.Any(r => r.NewRecord.Id != r.OldRecord.Id))
                throw new ArgumentException("one or more record pairs have mismatched Id properties");

            var diffList = new List<object>();
            foreach ((AirtableRecord NewRecord, AirtableRecord OldRecord) in recordPairs)
            {
                AirtableFieldsDictionary fieldDiff = NewRecord.Fields.ChangesFrom(OldRecord.Fields);
                if (fieldDiff.Count > 0)
                {
                    diffList.Add(new
                    {
                        NewRecord.Id,
                        Fields = fieldDiff
                    });
                }
            }

            return await BuildAirTableApiRequest<AirtableRecord>(HttpVerb.PATCH, table, diffList).ConfigureAwait(false);
        }
        #endregion ======= PATCH methods =======

        #region ======= DELETE methods =======
        public async Task<(string Id, bool Deleted)> Delete(string table, string id) =>
            (await Delete(table, new string[] { id }).ConfigureAwait(false)).Single();

        public async Task<List<(string Id, bool Deleted)>> Delete(string table, string[] ids)
        {
            if (String.IsNullOrEmpty(table)) throw new ArgumentNullException(nameof(table));
            if (ids == null || ids.Length == 0) throw new ArgumentNullException(nameof(ids));

            // Airtable API only accepts DELETE requests with a maximum of 10 ids per request, so we need to
            // split the request into chunks if more than 10 records are to be deleted
            var requestUris = new List<string>();
            var uriBuilder = new UriBuilder { Path = table };
            foreach (List<string> idSublist in ids.ToList().Split(10))
            {
                var queryParameters = new List<string>();
                foreach (string id in idSublist)
                {
                    queryParameters.Add(Uri.EscapeDataString($"records[]={id}"));
                }
                uriBuilder.Query = String.Join("&", queryParameters);
                requestUris.Add(uriBuilder.Uri.PathAndQuery);
            }

            return await BuildAirTableApiRequest<(string Id, bool Deleted)>(HttpVerb.DELETE, requestUris)
                .ConfigureAwait(false);
        }
        #endregion ======= DELETE methods =======

        #region ======= Private methods =======
        private async Task<List<T>> BuildAirTableApiRequest<T>(HttpVerb httpVerb, string requestUri, IEnumerable<object>? requestBodyData = null) =>
            await BuildAirTableApiRequest<T>(httpVerb, new string[] { requestUri }, requestBodyData).ConfigureAwait(false);

        private async Task<List<T>> BuildAirTableApiRequest<T>(HttpVerb httpVerb,
            IEnumerable<string> requestUris,
            IEnumerable<object>? requestBodyData = null)
        {
            bool useRequestBody = httpVerb.HasAttribute<UseRequestBodyAttribute>();
            if (useRequestBody && requestBodyData == null)
                throw new ArgumentNullException(nameof(requestBodyData), $"{nameof(requestBodyData)} is required for {httpVerb} requests");

            // Note: HttpClients created by an HttpClientFactory are managed and disposed of by the factory;
            // manually disposing of them is not required. 
            // See https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-2.1#httpclient-and-lifetime-management
            //HttpClient client = clientFactory.CreateClient("airtable");

            var apiCalls = new List<Task<HttpResponseMessage>>();

            if (useRequestBody)
            {
                // Airtable API only accepts requests with a maximum of 10 items, so we need to
                // split the request into chunks if more than 10 records are to be created or modified
                foreach (List<object> requestDataSublist in requestBodyData.ToList().Split(10))
                {
                    // Run API requests concurrently
                    apiCalls.Add(Task.Run(async () =>
                        await SendAirtableApiRequest(httpVerb, requestUris.Single(), CreateRequestBodyObject(requestDataSublist))
                        .ConfigureAwait(false)
                    ));
                }
            }
            else
            {
                foreach (string requestUri in requestUris)
                {
                    apiCalls.Add(Task.Run(async () => await SendAirtableApiRequest(httpVerb, requestUri).ConfigureAwait(false)));
                }
            }

            // Wait for all requests to complete before processing the results
            await Task.WhenAll().ConfigureAwait(false);

            // For each request, if it was successful, take the returned json, 
            // convert it to a List<AirtableRecord> and append it to returnList
            var returnList = new List<T>();
            foreach (Task<HttpResponseMessage> apiCall in apiCalls)
            {
                HttpResponseMessage response = apiCall.Result;

                if (apiCall.Status == TaskStatus.RanToCompletion)
                {
                    string responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    returnList.AddRange(JsonConvert.DeserializeObject<List<T>>(responseJson));
                }
            }

            return returnList;
        }

        private async Task<HttpResponseMessage> SendAirtableApiRequest(HttpVerb httpVerb, string requestUri, object? requestData = null)
        {
            string requestDataJson = JsonConvert.SerializeObject(requestData);
            using var request = new HttpRequestMessage // should be disposed by SendAsync
            {
                Method = new HttpMethod(httpVerb.ToString()),
                RequestUri = new Uri(requestUri),
            };
            if (requestData != null)
            {
                request.Content = new StringContent(requestDataJson, Encoding.UTF8, "application/json");  // StringContent doesn't require disposal
            }

            return await httpClient.Client.SendAsync(request).ConfigureAwait(false);
        }

        private (string, IEnumerable<T>) CreateRequestBodyObject<T>(IEnumerable<T> requestItems) => ("records", requestItems);

        private string BuildListQueryUriParameters(string[]? fields,
                                                   string? filterFormula,
                                                   int? maxRecords,
                                                   int? pageSize,
                                                   dynamic[]? sort,
                                                   string? view,
                                                   string? cellFormat,
                                                   string? timeZone,
                                                   string? userLocale,
                                                   string? offset)
        {
            var parameters = new List<string>();

            if (!(fields is null))
            {
                foreach (string field in fields)
                    parameters.Add(Uri.EscapeDataString($"fields[]={field}"));
            }

            if (!String.IsNullOrEmpty(filterFormula))
                parameters.Add(Uri.EscapeDataString($"filterByFormula=({filterFormula})"));
            
            if (maxRecords.HasValue)
                parameters.Add(Uri.EscapeDataString($"maxRecords={maxRecords}"));
            
            if (pageSize.HasValue)
                parameters.Add(Uri.EscapeDataString($"pageSize={pageSize}"));
            
            int sortNum = 0;
            
            if (!(sort is null))
            {
                foreach (var sortField in sort)
                {
                    parameters.Add(Uri.EscapeDataString($"sort[{sortNum}][field]={sortField.Field}"));
                    if (sortField.Direction != null)
                        parameters.Add(Uri.EscapeDataString($"sort[{sortNum}][direction]={sortField.Direction}"));
                    sortNum++;
                }
            }

            if (!String.IsNullOrEmpty(view))
                parameters.Add(Uri.EscapeDataString($"view={view}"));
            
            if (!String.IsNullOrEmpty(cellFormat))
                parameters.Add(Uri.EscapeDataString($"cellFormat={cellFormat}"));
            
            if (!String.IsNullOrEmpty(timeZone))
                parameters.Add(Uri.EscapeDataString($"timeZone={timeZone}"));

            if (!String.IsNullOrEmpty(userLocale))
                parameters.Add(Uri.EscapeDataString($"userLocale={userLocale}"));

            if (!String.IsNullOrEmpty(offset))
                parameters.Add(Uri.EscapeDataString($"offset={offset}"));

            return String.Join("&", parameters);
        }

        [return: MaybeNull]
        private AirtableRecord ConvertAirtableFieldValuesToPocos(AirtableRecord record,
            IEnumerable<(AirtableFieldValuePoco ObjectType, string[] FieldNames)>? conversionPairs) =>
            ConvertAirtableFieldValuesToPocos(new List<AirtableRecord> { record }, conversionPairs).SingleOrDefault();

        [return: MaybeNull]
        private List<AirtableRecord> ConvertAirtableFieldValuesToPocos(IEnumerable<AirtableRecord> records,
            IEnumerable<(AirtableFieldValuePoco ObjectType, string[] FieldNames)>? conversionPairs)
        {
            if (records is null) throw new ArgumentNullException(nameof(records));

            if (!records.Any() || conversionPairs?.Any() == false) return records.ToList();

            var returnList = new List<AirtableRecord>();
            foreach (AirtableRecord record in records)
            {
                if (record.Fields?.Any() == true)
                {
                    foreach ((AirtableFieldValuePoco ObjectType, string[] FieldNames) in conversionPairs!)
                    {
                        if (FieldNames is null || FieldNames.Length == 0)
                            break;

                        AirtableFieldsDictionary? fields;

                        switch (ObjectType)
                        {
                            case AirtableFieldValuePoco.Attachment:
                                fields = record.Fields.ValuesTo<AirtableAttachment>(FieldNames);
                                break;
                            case AirtableFieldValuePoco.Barcode:
                                fields = record.Fields.ValuesTo<AirtableBarcode>(FieldNames);
                                break;
                            case AirtableFieldValuePoco.Collaborator:
                                fields = record.Fields.ValuesTo<AirtableCollaborator>(FieldNames);
                                break;
                            default:
                                return null!;
                        }

                        if (fields is null)
                        {
                            //Log error
                            return null!;
                        }

                        record.Fields = fields;
                    }
                }
                returnList.Add(record);
            }
            return returnList;
        }

        #endregion ======= Private methods =======
    }
}
