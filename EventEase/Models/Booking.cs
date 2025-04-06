namespace EventEase.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Venues? Venues { get; set; }
        public Events? Events { get; set; }
        public DateTime bookingDate { get; set; }
        public string bookingStatus { get; set; }
        public string paymentMethod { get; set; }
        public string paymentStatus { get; set; }
    }
}
