using System.ComponentModel.DataAnnotations;
using WeddingPlanner.Validation;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WedId { get; set; }

        [Required, MinLength(1), MaxLength(50), Display(Name = "Wedder One")]
        public string? WedOne { get; set; }

        [Required, MinLength(1), MaxLength(50), Display(Name = "Wedder Two")]
        public string? WedTwo { get; set; }


        [Required, DataType(DataType.DateTime), Display(Name = "Wedding Date")]
        [PastDate]
        public DateTime WedDate { get; set; }

        [Required, MinLength(25), MaxLength(1500), Display(Name = "Wedding Adrress")]
        public string? WedAddy { get; set; }

        public int UserID { get; set; }

        public User? Creator { get; set; }

        public List<RSVP>? RSVPs { get; set; }
       
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime UpdatedOn { get; set; } = DateTime.Now;


    }
}
