using System.Collections.Generic;

namespace Airtable.ApiClient.Attributes
{
    internal class AirtableListResponse
    {
        public List<AirtableRecord> Records { get; }
        public string Offset { get; }
    }
}