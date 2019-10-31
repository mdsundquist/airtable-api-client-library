using System;

namespace Airtable.ApiClient.Entities
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal sealed class UseRequestBodyAttribute : Attribute
    {
        internal HttpEncoding HttpEncoding { get; }

        internal UseRequestBodyAttribute(HttpEncoding encoding) => HttpEncoding = encoding;
    }
}
