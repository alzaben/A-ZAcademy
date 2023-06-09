using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace A_ZAcademy.Models
{
	public class Slider:CommonProp
	{
		[Display(Name = "Slider Id")]
		public int SliderId { get; set; }
		[Display(Name = "Slider Title")]
		[Required(ErrorMessage = "Enter Title")]
		public string? SliderTitle { get; set; }
		[Display(Name = "Sub Title")]
		[Required(ErrorMessage = "Enter Sub Title")]
		public string? SliderSubTitle { get; set; }
		public string? TxtLink { get; set; }
		[Required(ErrorMessage = "Select Image")]
		public string? SliderImg { get; set; }
	}
}
