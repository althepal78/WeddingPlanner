using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MinLength(1), MaxLength(50), Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required, MinLength(2), MaxLength(50), Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required, MinLength(1), DataType(DataType.EmailAddress), EmailAddress, MaxLength(50), Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string? EMail { get; set; }


        [Required, MinLength(8), DataType(DataType.Password), MaxLength(2000), Display(Name = "Password")]
        public string? Password { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirm Password"), Compare("Password")]
        [NotMapped]
        public string? ConfirmPW { get; set; }

        public List<Wedding>? MyWeddings { get; set; }
        public List<RSVP>? Goers { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;


    }
}
