using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Attributes
{
    public class NullableIntegerAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value != null)
            {
                var int32 = Convert.ToDecimal(value);
                if (int32 == 0)
                {
                    return new ValidationResult("TESTE");
                }
            }
            
            return ValidationResult.Success;
        }
    }  
}
