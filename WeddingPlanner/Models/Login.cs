using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class Login
    {
        [Required, DataType(DataType.EmailAddress), EmailAddress, Display(Name = "Email")]
        public string? LoginEMail { get; set; }

        
        [Required,DataType(DataType.Password),  Display(Name = "Password")]
        public string? LoginPassword { get; set; }
    }
}
