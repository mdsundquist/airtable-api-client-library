using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Airtable.ApiClient.Extensions
{
    public static class ObjectExtensions
    {
        public static T ToType<T>(this object obj)// where T : IValidatableObject
        {
            // We want to stop serializing/deserializing if any errors are found, thus the use of try/catch
            // instead of a JsonSerializerSettings error-handling delegate
            //try
            //{
                T result = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
                return result.IsValid() ? result : default;
            //}
            //catch (JsonException)
            //{
            //    //TODO: log exception
            //    return default;
            //}
        }

        public static bool IsValid<T>(this T obj, bool validateAllProperties = true)
           // where T : IValidatableObject //=>
           // obj.Validate() == ValidationResult.Success;
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

        //public static IEnumerable<ValidationResult> Validate<T>(this T obj, ValidationContext validationContext)
        //  //  where T : IValidatableObject 
        //  => obj.Validate(new ValidationContext(obj));
    }
}