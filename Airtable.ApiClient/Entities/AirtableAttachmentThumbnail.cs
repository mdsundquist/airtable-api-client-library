using Airtable.ApiClient.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airtable.ApiClient.Entities
{

    public class AirtableAttachmentThumbnail : IEquatable<AirtableAttachmentThumbnail>
    {
        [Required, JsonProperty, JsonRequired]
        public string Url { get; set; } = "";

        [JsonProperty]
        public int? Width { get; set; }

        [JsonProperty]
        public int? Height { get; set; }

        public bool Equals(AirtableAttachmentThumbnail thumbnail) => this.ToString() == thumbnail?.ToString();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return this.Equals((obj as AirtableAttachmentThumbnail)!);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Url) ? 0 : Url.GetHashCode(StringComparison.Ordinal));
                hashCode = (hashCode * 486187739) + (Width?.GetHashCode() ?? 0);
                hashCode = (hashCode * 486187739) + (Height?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
