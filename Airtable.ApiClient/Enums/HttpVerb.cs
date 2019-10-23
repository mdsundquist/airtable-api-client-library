using System;

namespace Airtable.ApiClient.Attributes
{
    internal enum HttpVerb
    {
        GET,

        [UseRequestBody(HttpEncoding.JSON)]
        POST,

        [UseRequestBody(HttpEncoding.JSON)]
        PUT,

        [UseRequestBody(HttpEncoding.JSON)]
        PATCH,

        DELETE
    }

    internal enum HttpEncoding
    {
        None = 0,
        URI,
        JSON
    }
}