using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class BookingDetail
    {
        public long Id { get; set; }
        [Required]
        public string EventName { get; set; }
        [Required]
        public DateTime EventStartDate { get; set; }
        [Required]
        public string VenueName { get; set; }
        [Required]
        public int VenuesId { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime bookingDate { get; set; }
    }
}
