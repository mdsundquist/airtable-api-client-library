using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airtable.ApiClient.Entities
{
    public class AirtableCollaborator : IEquatable<AirtableCollaborator>, IValidatableObject
    {
        [JsonProperty]
        public string Id { get; internal set; }

        [Required, JsonRequired, EmailAddress]
        public string Email { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        public bool Equals(AirtableCollaborator other) => this.ToString() == other.ToString();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return this.Equals(obj as AirtableCollaborator);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Id) ? 0 : Id.GetHashCode());
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Id) ? 0 : Email.GetHashCode());
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Id) ? 0 : Name.GetHashCode());
                return hashCode;
            }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>(new List<ValidationResult> { new ValidationResult("Error") });
            //_ = Validator.TryValidateObject(instance: this,
            //                                validationContext: validationContext,
            //                                validationResults: validationResults,
            //                                validateAllProperties: false);
            return validationResults;
        }
    }
}