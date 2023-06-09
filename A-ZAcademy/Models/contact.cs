using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace A_ZAcademy.Models
{
    public class Contact:CommonProp
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage ="Please Enter Your Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Please Enter Your Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please Enter The Subject")]
        public string? Subject { get; set; }
        [Required(ErrorMessage = "Please Enter Yout Message")]
        public string? Message { get; set; }

    }
}
