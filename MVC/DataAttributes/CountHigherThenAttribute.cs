using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CountHigherThenAttribute : ValidationAttribute
    {
        private readonly int _count;

        public CountHigherThenAttribute(int count)
        {
            _count = count;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is ICollection)
            {
                var list = value as ICollection;
                return list.Count > _count ? ValidationResult.Success : new ValidationResult(ErrorMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}