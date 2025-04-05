using Microsoft.EntityFrameworkCore;
namespace EventEase.Models
{
    public class ApplicationDBContexts : DbContext
    {
        public ApplicationDBContexts(DbContextOptions<ApplicationDBContexts> options) : base(options)
        { 
        }

        public DbSet<Venues> Venues { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Booking> Booking { get; set; }
    }
}
