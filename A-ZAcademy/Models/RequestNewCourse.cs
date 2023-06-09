using A_ZAcademy.Models.SharedProp;

namespace A_ZAcademy.Models
{
    public class RequestNewCourse:CommonProp
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? RequestLink { get; set; }

    }
}
