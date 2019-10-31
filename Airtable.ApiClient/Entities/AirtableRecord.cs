using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airtable.ApiClient.Entities
{
    public class AirtableRecord : IEquatable<AirtableRecord>, IValidatableObject
    {
        [JsonProperty]
        public string Id { get; internal set; }

        [JsonProperty]
        public DateTime? CreatedTime { get; internal set; }

        [Required, JsonProperty, JsonRequired]
        public AirtableFieldsDictionary Fields { get; set; }

        public AirtableRecord()
        {
        }

        public AirtableRecord(string id) : this(id, null)
        {
        }

        public AirtableRecord(string id, DateTime? createdTime)
        {
            Id = id;
            CreatedTime = createdTime;
        }

        public bool Equals(AirtableRecord other) => this.ToString() == other.ToString();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return this.Equals(obj as AirtableRecord);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Id) ? 0 : Id.GetHashCode());
                hashCode = (hashCode * 486187739) + (CreatedTime?.GetHashCode() ?? 0);
                hashCode = (hashCode * 486187739) + (Fields?.GetHashCode() ?? 0);
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
