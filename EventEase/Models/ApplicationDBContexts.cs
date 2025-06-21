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
        public DbSet<BookingDetail> BookingDetail { get; set; }
        public DbSet<EventType> EventType { get; set; }
    }
}
