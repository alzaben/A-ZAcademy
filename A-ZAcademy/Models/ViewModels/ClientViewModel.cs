using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace A_ZAcademy.Models.ViewModels
{
    public class ClientViewModel:CommonProp
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
        public IFormFile? Img { get; set; }
        public string? opinion { get; set; }
    }
}
