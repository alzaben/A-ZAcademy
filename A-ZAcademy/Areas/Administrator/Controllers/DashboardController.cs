using A_ZAcademy.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace A_ZAcademy.Areas.Administrator.Controllers
{
    [Area("Administrator")]
	[Authorize(Roles = "Admin")]

	public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private RoleManager<IdentityRole> _RoleManager;
        public DashboardController(ApplicationDbContext context, RoleManager<IdentityRole> RoleManager)
        {
            _context = context;
            _RoleManager = RoleManager;
        }
        /*  public IActionResult Count()
          {
              int courses = _context.courses.Count();
              int coursesrequests = _context.coursesRequests.Count();

              var counts = new Dictionary<string, int>
      {
          { "CoursesCount", courses },
          { "OrderCount", coursesrequests }
      };

              return View(counts);
          }*/
        public IActionResult Count()
        {
            int recordCount = _context.coursesRequests.Count();
            return View(recordCount);
        }

        public IActionResult Index()
        {
            int courses = _context.courses.Count();
            int coursesrequests = _context.coursesRequests.Count();
            int newcourse = _context.requestNewCoursesForm.Count();
            int instructors = _context.instructors.Count();
            int instructorsReq=_context.InstructorRequest.Count();
            int studentRoleCount = _RoleManager.Roles
        .Where(r => r.Name == "Student") // Replace "Student" with the name of the desired role
        .Join(_context.UserRoles, r => r.Id, ur => ur.RoleId, (r, ur) => ur.UserId)
        .Join(_context.Users, ur => ur, u => u.Id, (ur, u) => u)
        .Count();


            var counts = new Dictionary<string, int>
      {
          { "CoursesCount", courses },
          { "CourseRequestCount", coursesrequests },
          { "newcourse", newcourse },
          { "instructors", instructors },
          { "instructorsReq", instructorsReq },
          { "StudentRoleCount", studentRoleCount }

      };

            return View(counts);
        }
    }
}
