using System.Xml.Linq;

namespace A_ZAcademy.Models.ViewModels
{
    public class GetNextPage
    {
       
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            public List<Course> Data { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            public bool HasNextPage { get; set; }
            public string? NextPageUrl { get; set; }
        

    }
}
