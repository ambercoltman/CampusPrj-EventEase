namespace EventEase.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int VenuesId { get; set; }
        public int EventsId { get; set; }
        public DateTime bookingDate { get; set; }
        public string bookingStatus { get; set; }
        public string paymentMethod { get; set; }
        public string paymentStatus { get; set; }
    }
}
