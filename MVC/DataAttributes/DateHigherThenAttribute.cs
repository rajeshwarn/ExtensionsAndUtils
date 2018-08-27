using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Attributes
{
    public class DateHigherThenAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;

        public DateHigherThenAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _otherProperty);
        }

        protected override ValidationResult IsValid(object firstValue, ValidationContext validationContext)
        {
            if (validationContext != null)
            {
                var firstComparable = firstValue as IComparable;
                var secondComparable = GetSecondComparable(validationContext);

                if (firstComparable != null && secondComparable != null)
                {
                    if (firstComparable.CompareTo(secondComparable) < 0)
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }

            return ValidationResult.Success;
        }

        private IComparable GetSecondComparable(ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_otherProperty);
            return propertyInfo != null ? propertyInfo.GetValue(validationContext.ObjectInstance, null) as IComparable : null;
        }
    }
}
