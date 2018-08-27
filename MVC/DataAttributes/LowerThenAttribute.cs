using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Attributes
{
    public class LowerThenAttribute : ValidationAttribute
    {
        private readonly object _expectedValue;

        public LowerThenAttribute(object expectedValue)
        {
            _expectedValue = expectedValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            IComparable comparable = (IComparable)value;
            return comparable != null && comparable.CompareTo(_expectedValue) > 0
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage);
        }
    }
}