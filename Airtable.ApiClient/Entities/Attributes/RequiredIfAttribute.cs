using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Airtable.ApiClient.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal sealed class RequiredIfAttribute : ValidationAttribute
    {
        internal string PropertyName { get; }
        internal object? RequiredValue { get; }

        internal RequiredIfAttribute(string propertyName, object? value)
        {
            PropertyName = propertyName;
            RequiredValue = value;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(PropertyName);

            var otherPropertyValue = otherPropertyInfo?.GetValue(validationContext.ObjectInstance);

            bool propertyIsRequired;

            if (otherPropertyInfo.PropertyType == typeof(string))
            {
                propertyIsRequired = String.IsNullOrEmpty((string)otherPropertyValue) && String.IsNullOrEmpty(RequiredValue as string);
            }
            else
            {
                propertyIsRequired = otherPropertyValue == RequiredValue;
            }

            if (propertyIsRequired && String.IsNullOrEmpty(value as string))
                return new ValidationResult(String.Format(ErrorMessageString, validationContext.DisplayName, otherPropertyInfo.Name));

            return ValidationResult.Success;
        }
    }
}
