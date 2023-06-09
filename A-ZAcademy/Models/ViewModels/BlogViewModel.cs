using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace A_ZAcademy.Models.ViewModels
{
    public class BlogViewModel:CommonProp
    {
        public int BlogId { get; set; }
        [Display(Name = "Blog Title")]
        [Required(ErrorMessage = "Enter Blog Title")]
        public string? BlogTitle { get; set; }
        [Required(ErrorMessage = "Select Image")]
        public IFormFile? BlogImg { get; set; }
        [Display(Name = "Blog Description")]
        [DataType(DataType.MultilineText)]
        [Required]
        public string? BlogDescription { get; set; }
    }
}
