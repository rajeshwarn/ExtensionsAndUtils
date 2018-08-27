using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateHigherThenTodayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result = ValidationResult.Success;
            object id = null;
            
            PropertyInfo propertyInfo = validationContext.ObjectType.GetProperty("Id");
            if (propertyInfo != null)
            {
                id = propertyInfo.GetValue(validationContext.ObjectInstance, null);
            }

            if (id == null)
            {
                if (value is DateTime)
                {
                    var dt = (DateTime) value;
                    result = dt < DateTime.UtcNow.Date ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
                }
                else
                {
                    if (value != null)
                    {
                        throw new Exception("Invalid data type for this attribute");
                    }

                    result = new ValidationResult(ErrorMessage);
                }
            }

            return result;
        }
    }
}
