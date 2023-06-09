using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace A_ZAcademy.Models
{
	public class Setting:CommonProp
	{
        public int SettingId { get; set; }
        [Required(ErrorMessage ="Enter Email")]
		[Display(Name = "Email")]
		public string? Email { get; set; }
		[Display(Name = "Location")]
		public string? Location { get; set; }
		[Required(ErrorMessage = "Enter Phone Number")]
		[Display(Name = "PhoneNumber")]
		public string? PhoneNumber { get; set;}
		[Display(Name = "WebName")]
        public string? WebName { get; set; }
    }
}
