using System.ComponentModel.DataAnnotations;

namespace A_ZAcademy.Models
{
    public class MPage
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " Image")]
        public string? Img { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Enter Title")]
        public string? Title { get; set; }
        public string? STitle { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Enter Description")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }


    }
}
