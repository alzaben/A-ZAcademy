using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace A_ZAcademy.Models
{
	public class Instructor:CommonProp
	{
		public int InstructorId { get; set; }
		[Required(ErrorMessage = "Instructor Name")]
		[Display(Name = "Instructor Name")]
		public string? InstructorName { get; set; }
		[Required(ErrorMessage = "Instructor Picture")]
		[Display]
		public string? InstructorImg { get; set; }
		[Required]
		public string? Position { get; set; }
		public string? Fb { get; set; }
		public string? Tw { get; set; }
		public string? LinkedIN { get; set; }
	}
}
