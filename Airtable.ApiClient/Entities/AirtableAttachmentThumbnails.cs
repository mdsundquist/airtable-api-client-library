using Airtable.ApiClient.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airtable.ApiClient.Entities
{

    public class AirtableAttachmentThumbnails : IEquatable<AirtableAttachmentThumbnails>
    {
        [RequiredIf(propertyName: "Large", value: null), JsonProperty]
        public AirtableAttachmentThumbnail? Small { get; set; }

        [JsonProperty]
        public AirtableAttachmentThumbnail? Large { get; set; }

        public bool Equals(AirtableAttachmentThumbnails other) => this.ToString() == other?.ToString();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return this.Equals((obj as AirtableAttachmentThumbnails)!);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                hashCode = (hashCode * 486187739) + (Small?.GetHashCode() ?? 0);
                hashCode = (hashCode * 486187739) + (Large?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
