namespace EventEase.Models
{
    public class Events
    {
        public int Id { get; set; }
        public string EventName{ get; set; }
        public DateTime EventStartDate{ get; set; }
        public DateTime EventEndDate{ get; set; }
        public string Description{ get; set; }
    }
}
