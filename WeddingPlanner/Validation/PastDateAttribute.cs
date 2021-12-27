using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Validation
{
    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            if(DateTime.Now > (DateTime)value)
            {
                return new ValidationResult("You can't schedule a wedding in the past fool");
            }

            return ValidationResult.Success;
        }
    }
}
