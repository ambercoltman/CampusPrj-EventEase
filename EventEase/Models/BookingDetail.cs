namespace EventEase.Models
{
    public class BookingDetail
    {
        public long Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventStartDate { get; set; }
        public string VenueName { get; set; }
        public string Location { get; set; }
        public DateTime bookingDate { get; set; }
    }
}
