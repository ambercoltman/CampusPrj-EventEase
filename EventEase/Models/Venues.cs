using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Venues
    {
        public int Id { get; set; }
        public string VenueName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public string ImageUrl { get; set; } 
    }
}
