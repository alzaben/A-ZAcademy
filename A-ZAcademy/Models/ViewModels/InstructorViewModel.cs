using A_ZAcademy.Models.SharedProp;
using System.ComponentModel.DataAnnotations;

namespace A_ZAcademy.Models.ViewModels
{
    public class InstructorViewModel :CommonProp
    {
        public int InstructorId { get; set; }
        [Required(ErrorMessage = "Instructor Name")]
        [Display(Name = "Instructor Name")]
        public string? InstructorName { get; set; }
        [Required(ErrorMessage = "Instructor Picture")]
        [Display]
        public IFormFile? InstructorImg { get; set; }
        [Required]
        public string? Position { get; set; }
        public string? Fb { get; set; }
        public string? Tw { get; set; }
        public string? LinkedIN { get; set; }
    }
}
