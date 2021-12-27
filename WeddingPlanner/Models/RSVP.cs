using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class RSVP
    {
        [Key]
        public int RSVPId { get; set; }

        public int? WeddingID { get; set; }
        public int? UserID { get; set; }

        public User Guests { get; set; }    

        public Wedding Wedding { get; set; }

    }
}
