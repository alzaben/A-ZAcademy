using A_ZAcademy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace A_ZAcademy.Data
{
	public class ApplicationDbContext:IdentityDbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        public DbSet<Menu> menus { get; set; }
        public DbSet<Blog> blogs { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Setting> settings { get; set; }
        public DbSet<Slider> sliders { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<CourseRequest> coursesRequests { get; set; }
        public DbSet<RequestNewCourse> requestNewCourses { get; set; }
        public DbSet<RequestNewCourseForm> requestNewCoursesForm { get; set;}
        public DbSet<Contact> contacts { get; set; }
        public DbSet<A_ZAcademy.Models.InstructorRequest> InstructorRequest { get; set; } = default!;
        public DbSet<MPage> pages { get; set; }
    }
}
