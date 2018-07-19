using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequiredConditionalAttribute: ValidationAttribute
    {
        private readonly string _conditionProperty;

        public RequiredConditionalAttribute(string conditionProperty)
        {
            _conditionProperty = conditionProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            bool firstComparable = value != null;
            bool secondComparable = GetSecondComparable(validationContext);

            if (!secondComparable && firstComparable)
                return new ValidationResult(ErrorMessage);
            if (secondComparable && !firstComparable)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }

        private bool GetSecondComparable(ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_conditionProperty);
            if (propertyInfo != null)
            {
                var secondValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);
                return secondValue != null;
            }
            else
            {
                return false;
            }
        }
    }
}
