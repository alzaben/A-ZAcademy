using Microsoft.EntityFrameworkCore;

namespace A_ZAcademy.Models
{
    public class InstructorRequest
    {
        
        public int InstructorRequestId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime BirthDay { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Resume { get; set; }
        public Genders Gender { get; set; }

        public enum Genders
        {
            Male, Female
        }
    }
}
