using System;
using System.ComponentModel.DataAnnotations;
using Shared;

namespace Model.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TimeHigherThenAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;
        private readonly ITimeValidator _validator;

        public TimeHigherThenAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
            _validator = new DaysValidator();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var val = value as Time;
            var compareTo = GetSecondComparable(validationContext);

            if (!_validator.TwoHigherThanOne(compareTo, val))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        private Time GetSecondComparable(ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_otherProperty);
            return propertyInfo != null ? propertyInfo.GetValue(validationContext.ObjectInstance, null) as Time : null;
        }
    }
}
