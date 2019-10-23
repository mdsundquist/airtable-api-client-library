using System;

namespace Airtable.ApiClient.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal sealed class UseRequestBodyAttribute : Attribute
    {
        internal HttpEncoding HttpEncoding { get; }

        internal UseRequestBodyAttribute(HttpEncoding encoding) => HttpEncoding = encoding;
    }
}
