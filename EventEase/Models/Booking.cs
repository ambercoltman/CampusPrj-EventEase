using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Booking
    {
        public int Id { get; set; }
        [Required]
        public int VenuesId { get; set; }
        [Required]
        public int EventsId { get; set; }
        [Required]
        public DateTime bookingDate { get; set; }
        [Required]
        public string bookingStatus { get; set; }
        [Required]
        public string paymentMethod { get; set; }
        [Required]
        public string paymentStatus { get; set; }
    }
}
