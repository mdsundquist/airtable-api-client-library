using System.Collections.Generic;

namespace Airtable.ApiClient.Entities
{
    internal class AirtableListResponse
    {
        public List<AirtableRecord>? Records { get; }
        public string? Offset { get; }
    }
}