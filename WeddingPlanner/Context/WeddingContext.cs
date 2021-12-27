using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Context
{
    public class WeddingContext : DbContext
    {
        public WeddingContext(DbContextOptions<WeddingContext> options ) : base( options ){}

        public DbSet<User> Users { get; set; }

        public DbSet<Wedding> Weddings { get; set; }

        public DbSet<RSVP> RSVRs { get; set; }
    }
}
