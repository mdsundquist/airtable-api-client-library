using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airtable.ApiClient.Attributes
{
    public class AirtableAttachment : IEquatable<AirtableAttachment>, IValidatableObject
    {
        [JsonProperty]
        public string Id { get; internal set; }

        [Required, JsonRequired, Url]
        public string Url { get; set; }

        [JsonProperty]
        public string Filename { get; set; }

        [JsonProperty]
        public int? Size { get; set; }

        [JsonProperty]
        public string Type { get; set; }

        [JsonProperty]
        public int? Width { get; set; }

        [JsonProperty]
        public int? Height { get; set; }

        [JsonProperty]
        public AirtableAttachmentThumbnails Thumbnails { get; set; }

        public bool Equals(AirtableAttachment attachment) => this.ToString() == attachment.ToString();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return this.Equals(obj as AirtableAttachment);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Id) ? 0 : Id.GetHashCode());
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Url) ? 0 : Url.GetHashCode());
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Filename) ? 0 : Filename.GetHashCode());
                hashCode = (hashCode * 486187739) + (Size?.GetHashCode() ?? 0);
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Type) ? 0 : Type.GetHashCode());
                hashCode = (hashCode * 486187739) + (Width?.GetHashCode() ?? 0);
                hashCode = (hashCode * 486187739) + (Height?.GetHashCode() ?? 0);
                hashCode = (hashCode * 486187739) + (Thumbnails?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            _ = Validator.TryValidateObject(instance: this,
                                                       validationContext: validationContext,
                                                       validationResults: validationResults,
                                                       validateAllProperties: false);
            return validationResults;
        }
    }

    public class AirtableAttachmentThumbnails : IEquatable<AirtableAttachmentThumbnails>, IValidatableObject
    {
        [JsonProperty]
        public AirtableAttachmentThumbnail Small { get; set; }

        [JsonProperty]
        public AirtableAttachmentThumbnail Large { get; set; }

        public bool Equals(AirtableAttachmentThumbnails other) => this.ToString() == other.ToString();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return this.Equals(obj as AirtableAttachmentThumbnails);
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = null;
            if (Small is null && Large is null)
            {
                validationResults = new List<ValidationResult>
                {
                    new ValidationResult($"One or both of the {this.GetType()} Small or Large properties must have a value",
                        new string[] { "Small", "Large" })
                };
            }
            return validationResults;
        }
    }

    public class AirtableAttachmentThumbnail : IEquatable<AirtableAttachmentThumbnail>, IValidatableObject
    {
        [Required, JsonProperty, JsonRequired]
        public string Url { get; set; }

        [JsonProperty]
        public int? Width { get; set; }

        [JsonProperty]
        public int? Height { get; set; }

        public bool Equals(AirtableAttachmentThumbnail thumbnail) => this.ToString() == thumbnail.ToString();

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Url) ? 0 : Url.GetHashCode());
                hashCode = (hashCode * 486187739) + (Width?.GetHashCode() ?? 0);
                hashCode = (hashCode * 486187739) + (Height?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            _ = Validator.TryValidateObject(instance: this,
                                                       validationContext: validationContext,
                                                       validationResults: validationResults,
                                                       validateAllProperties: false);
            return validationResults;
        }
    }
}
