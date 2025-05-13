using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Booking
    {
        //Quinn states that the [Rquired] attributes that will show a message to the user to fill out the fields.
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

//Quin,M. 2024. ASP.NET MVC: Form Validation,[blog] 04 February.Available at: < https://medium.com/%40MJQuinn/asp-net-mvc-form-validation-d5473229f138 > [Accessed 11 May 2025].
