using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace A_ZAcademy.Models
{
	public class Category:CommonProp
	{
        public int CategoryID { get; set; }
		[Required]
		[Display(Name = "CategoryName")]
		public string? CategoryName { get; set; }
        public List<Course>? Courses { get; set; }
        public string? Img { get; set; }
        public int CourseCount => Courses?.Count ?? 0;

    }
}
