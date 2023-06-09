using System.ComponentModel.DataAnnotations;

namespace A_ZAcademy.Models.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Please Enter Your Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
