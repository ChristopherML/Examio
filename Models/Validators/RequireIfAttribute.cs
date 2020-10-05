using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Examio.Models.Validators
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessageFormatString = "The {0} field is required.";
        private readonly string[] _dependentProperties;

        public RequiredIfAttribute(string[] dependentProperties)
        {
            _dependentProperties = dependentProperties;
            ErrorMessage = DefaultErrorMessageFormatString;
        }

        private bool IsValueRequired(string checkValue, Object currentValue)
        {
            if (checkValue.Equals("!null", StringComparison.InvariantCultureIgnoreCase))
            {
                return currentValue != null;
            }

            return checkValue.Equals(currentValue);
        }

        protected override ValidationResult IsValid(Object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            bool valueRequired = false;

            foreach (string s in _dependentProperties)
            {
                var fieldValue = s.Split(',').ToList().Select(k => k.Trim()).ToArray();

                Object propertyValue = type.GetProperty(fieldValue[0]).GetValue(instance, null);

                valueRequired = IsValueRequired(fieldValue[1], propertyValue);
            }

            if (valueRequired)
            {
                return value != null
                    ? ValidationResult.Success
                    : new ValidationResult(context.DisplayName + " required. ");
            }
            return ValidationResult.Success;
        }
    }
}
