using Microsoft.EntityFrameworkCore;
using EventEase.Models;
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
        public DbSet<EventEase.Models.BookingDetail> BookingDetail { get; set; } = default!;
    }
}
