using System.ComponentModel.DataAnnotations;

namespace Shared.Attributes
{
    public class RequiredInServerAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            return value == null ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
        }
    }

    
}
