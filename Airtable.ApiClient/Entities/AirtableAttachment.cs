using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Airtable.ApiClient.Entities
{
    public class AirtableAttachment : IEquatable<AirtableAttachment>
    {
        [JsonProperty]
        public string? Id { get; internal set; }

        [Required, JsonRequired, Url]
        public string Url { get; set; } = "";

        [JsonProperty]
        public string? Filename { get; set; }

        [JsonProperty]
        public int? Size { get; set; }

        [JsonProperty]
        public string? Type { get; set; }

        [JsonProperty]
        public int? Width { get; set; }

        [JsonProperty]
        public int? Height { get; set; }

        [JsonProperty]
        public AirtableAttachmentThumbnails? Thumbnails { get; set; }

        public bool Equals(AirtableAttachment attachment) => this.ToString() == attachment?.ToString();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return this.Equals((obj as AirtableAttachment)!);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Id) ? 0 : Id.GetHashCode(StringComparison.Ordinal));
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Url) ? 0 : Url.GetHashCode(StringComparison.Ordinal));
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Filename) ? 0 : Filename.GetHashCode(StringComparison.Ordinal));
                hashCode = (hashCode * 486187739) + (Size?.GetHashCode() ?? 0);
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Type) ? 0 : Type.GetHashCode(StringComparison.Ordinal));
                hashCode = (hashCode * 486187739) + (Width?.GetHashCode() ?? 0);
                hashCode = (hashCode * 486187739) + (Height?.GetHashCode() ?? 0);
                hashCode = (hashCode * 486187739) + (Thumbnails?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
