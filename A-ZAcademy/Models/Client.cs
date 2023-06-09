using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace A_ZAcademy.Models
{
	public class Client:CommonProp
	{
		public int ClientId { get; set; }
		[Display(Name = "Client Name")]
		[Required(ErrorMessage = "Enter Client Name")]
		public string? ClientName { get; set; }
		[Display(Name = "Client Position")]
		[Required(ErrorMessage = "Entert Client Position")]
		public string? ClientPosition { get; set; }
		[Required(ErrorMessage = "Select Image")]
		[Display(Name = "Img")]
		public string? Img { get; set; }
        public string? opinion { get; set; }

    }
}
