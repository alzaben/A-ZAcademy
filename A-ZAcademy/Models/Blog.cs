using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace A_ZAcademy.Models
{
	public class Blog:CommonProp
	{
		public int BlogId { get; set; }
		[Display(Name = "Blog Title")]
		[Required(ErrorMessage = "Enter Blog Title")]
		public string? BlogTitle { get; set; }
		[Required(ErrorMessage = "Select Image")]
		public string? BlogImg { get; set; }
		[Display(Name = "Blog Description")]
		[DataType(DataType.MultilineText)]
		[Required]
		public string? BlogDescription { get; set; }
	}
}
