using System.ComponentModel.DataAnnotations;

namespace A_ZAcademy.Models
{
    public class CourseRequest
    {
        
        public int CourseRequestID { get; set; }
        [Required(ErrorMessage = "Please Enter Your Name")]
        [Display(Name = "Course Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please Enter Your Email")]
        [Display(Name = "Email address")]
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

    }
}
