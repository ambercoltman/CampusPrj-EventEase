using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Venues
    {
        //Quinn states that the [Rquired] attributes that will show a message to the user to fill out the fields.
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

//Quin,M. 2024. ASP.NET MVC: Form Validation,[blog] 04 February.Available at: < https://medium.com/%40MJQuinn/asp-net-mvc-form-validation-d5473229f138 > [Accessed 11 May 2025].
