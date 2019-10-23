using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Airtable.ApiClient.Attributes
{
    public class AirtableBarcode : IEquatable<AirtableBarcode>//, IValidatableObject
    {
        [RequiredIf(propertyName: "Type", value: null), JsonProperty]
        public string Text { get; set; }

        [JsonProperty]
        //[RequiredIf(propertyName: "Text", value: null), JsonProperty]
        public string Type { get; set; }

        public bool Equals(AirtableBarcode other) => this.ToString() == other.ToString();

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (this.GetType() != obj.GetType()) return false;
            return this.Equals(obj as AirtableBarcode);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 17;
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Text) ? 0 : Text.GetHashCode());
                hashCode = (hashCode * 486187739) + (String.IsNullOrEmpty(Type) ? 0 : Type.GetHashCode());
                return hashCode;
            }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);

        public static bool operator ==(AirtableBarcode left, AirtableBarcode right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AirtableBarcode left, AirtableBarcode right)
        {
            return !(left == right);
        }

        //public bool IsValid() => Validate() == ValidationResult.Success;

        //public IEnumerable<ValidationResult> Validate() => Validate(new ValidationContext(this));

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    List<ValidationResult> validationResults = null;
        //    if (String.IsNullOrEmpty(Text) && String.IsNullOrEmpty(Type))
        //    {
        //        validationResults = new List<ValidationResult>
        //        {
        //            new ValidationResult($"One or both of the {this.GetType()} Text or Type properties must have a value",
        //                new string[] { "Text", "Type" })
        //        };
        //    }
        //    return validationResults;
        //}
    }
}
