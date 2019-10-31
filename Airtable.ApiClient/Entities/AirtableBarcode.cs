using Airtable.ApiClient.Attributes;
using Newtonsoft.Json;
using System;

namespace Airtable.ApiClient.Entities
{
    public class AirtableBarcode : IEquatable<AirtableBarcode>
    {
        [RequiredIf(propertyName: "Type", value: null), JsonProperty]
        public string? Text { get; set; }

        [JsonProperty]
        public string? Type { get; set; }

        public bool Equals(AirtableBarcode other) => this.ToString() == other?.ToString();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return this.Equals((obj as AirtableBarcode)!);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Text) ? 0 : Text.GetHashCode(StringComparison.Ordinal));
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Type) ? 0 : Type.GetHashCode(StringComparison.Ordinal));
                return hashCode;
            }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
