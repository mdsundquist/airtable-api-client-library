using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Airtable.ApiClient.Extensions
{
    public static class ObjectExtensions
    {
        [return: MaybeNull]
        public static T ToType<T>(this object obj)
        {
            // We want to stop serializing/deserializing if any errors are found, thus the use of try/catch
            // instead of a JsonSerializerSettings error-handling delegate
            try
            {
                T result = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
                return result.IsValid() ? result : default;
            }
            catch (JsonException)
            {
                //TODO: log exception
                #pragma warning disable CS8653 // A default expression introduces a null value for a type parameter.
                return default; // Calling method is warned to check for null by MaybeNull attribute
                #pragma warning restore CS8653 // A default expression introduces a null value for a type parameter.
            }
        }

        public static bool IsValid<T>(this T obj, bool validateAllProperties = true)
        {
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(
                instance: obj,
                validationContext: new ValidationContext(obj),
                validationResults: validationResults,
                validateAllProperties: validateAllProperties
            );

            // TODO: Log validationResults

            return isValid;
        }
    }
}