using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace A_ZAcademy.Models
{
    public class RequestNewCourseForm : CommonProp
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Your Name")]
        [Display(Name ="Your Name")]
        public string? Name { get; set; }
        [EmailAddress]
        [Required(ErrorMessage ="Please Enter Your Email")]
        [Display(Name = "Your Email")]
        public string? Email { get; set; }
        [Display(Name = "Your Phone Number")]
        public string? Phone { get; set; }
        [Display(Name = "Course Name")]
        [Required(ErrorMessage ="Please Enter Course Name")]    

        public string? CourseName { get; set; }
        [Display(Name = "Describe the course")]
        [DataType(DataType.MultilineText)]
        public string? CourseDescription { get; set; }
        [Display(Name = "Expected Price")]
        public int ExpectedPrice { get; set; }

    }
}
