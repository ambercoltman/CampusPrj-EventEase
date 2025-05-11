using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Events
    {
        public int Id { get; set; }
        [Required]
        public string EventName{ get; set; }
        [Required]
        public DateTime EventStartDate{ get; set; }
        [Required]
        public DateTime EventEndDate{ get; set; }
        [Required]
        public string Description{ get; set; }
    }
}
