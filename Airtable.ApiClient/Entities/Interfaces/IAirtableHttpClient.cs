using System.Net.Http;

namespace Airtable.ApiClient.Entities
{
    // TODO: implement strongly-typed httpClient
    public interface IAirtableHttpClient
    {
        HttpClient Client { get; }
    }
}
