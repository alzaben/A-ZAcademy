using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace A_ZAcademy.Models
{
	public class Course:CommonProp
	{
		public int CourseId { get; set; }
		[Display(Name = "Course Title")]
		[Required(ErrorMessage = "Enter Title")]
		public string? CourseTitle { get; set; }
		[Display(Name = "Course Description")]
		[Required(ErrorMessage = "Enter Description")]
		[DataType(DataType.MultilineText)]
		public string? CourseDescription { get; set; }
		[Required(ErrorMessage = "Enter Price")]
		[Display(Name = "Course Price")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Course Image")]
		public string? Img { get; set; }
		[DataType(DataType.Date)]
		public DateTime StartDate { get; set; }
		[Display(Name = "Start Time")]
		[Required(ErrorMessage = "Enter Start time")]
		public TimeSpan StartTime { get; set; }
		[Display(Name = "Duration")]
		[Required(ErrorMessage = "Enter Duration")]
		public string? Duration { get; set; }
		[Display(Name = "Hours")]
		[Required(ErrorMessage = "Enter Hours")]
		public int Hours { get; set; }
		[Display(Name = "Rate")]
		public int Rate { get; set; }
        public int? StudentNumber { get; set; }

        [ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category? Category { get; set; }
	}
}
