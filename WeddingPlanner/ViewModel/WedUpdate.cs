using System.ComponentModel.DataAnnotations;
using WeddingPlanner.Validation;

namespace WeddingPlanner.ViewModel
{
    public class WedUpdate
    {
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


    }
}
