using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{
    public class Events
    {
        //Quinn states that the [Required] attributes that will show a message to the user to fill out the fields.
        public int Id { get; set; }
        [Required]
        public string EventName{ get; set; }
        [Required]
        public DateTime EventStartDate{ get; set; }
        [Required]
        public DateTime EventEndDate{ get; set; }
        [Required]
        public string Description{ get; set; }
        [Required]
        public int EventTypeID { get; set; }
    }
}

//Quin,M. 2024. ASP.NET MVC: Form Validation,[blog] 04 February.Available at: < https://medium.com/%40MJQuinn/asp-net-mvc-form-validation-d5473229f138 > [Accessed 11 May 2025].
