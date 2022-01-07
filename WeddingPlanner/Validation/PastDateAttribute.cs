using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Validation
{
    public class PastDateAttribute : ValidationAttribute, IClientModelValidator
    {
        public PastDateAttribute()
        {
            ErrorMessage = "You can't schedule a wedding in the past fool";
        }
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-pastdate", ErrorMessage);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(DateTime.Now > (DateTime)value)
            {
                return new ValidationResult("You can't schedule a wedding in the past fool");
            }
            if(ValidationResult.Success == null)
            {
                return ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}
