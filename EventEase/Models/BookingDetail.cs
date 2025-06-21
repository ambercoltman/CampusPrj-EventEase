using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class BookingDetail
    {
        //Quinn states that the [Rquired] attributes that will show a message to the user to fill out the fields.
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
        [Required]
        public bool isAvailable { get; set; }
        [Required]
        public string EventType { get; set; }
    }
}

//Quin,M. 2024. ASP.NET MVC: Form Validation,[blog] 04 February.Available at: < https://medium.com/%40MJQuinn/asp-net-mvc-form-validation-d5473229f138 > [Accessed 11 May 2025].

