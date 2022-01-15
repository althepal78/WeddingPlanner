using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Validation
{
    public class PastDateAttribute : ValidationAttribute
    {
        public string getErrorMessage() => $"You can't schedule a wedding in the past fool";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (DateTime.Now > DateTime.Parse(value.ToString()))
            {
                return new ValidationResult(getErrorMessage());
            }
            return ValidationResult.Success;
        }

    }
}
