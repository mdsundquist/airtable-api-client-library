using System.Net.Http;

namespace Airtable.ApiClient.Attributes
{
    public interface IAirtableHttpClient
    {
        HttpClient Client { get; }
    }
}
