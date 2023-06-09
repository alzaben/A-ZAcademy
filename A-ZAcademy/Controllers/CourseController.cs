using A_ZAcademy.Data;
using A_ZAcademy.Models;
using A_ZAcademy.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace A_ZAcademy.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Courses(int? pageNumber)
        {
            int pageSize = 6;


            return View(PaginatedList<Course>.Create(await _context.courses.ToListAsync(), pageNumber ?? 1, pageSize));
        }
        public async Task<IActionResult> PageDetails(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = await _context.courses
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }
			var courseViewModel = new CourseReViewModel
			{
				Course = course
			};
			return View(courseViewModel);
        }
        [HttpGet]
        public IActionResult CourseRequest()
        {
			
			return View();
        }
        [HttpPost]
        public async Task<IActionResult> CourseRequest(CourseReViewModel model)
        {
            if (ModelState.IsValid)
            {

                _context.Add(model.CourseRequest);

                await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Home");
			}

            var data = _context.courses.OrderByDescending(x => x.CreationDate);//need to back 
            return View(data);
        }

    }
}
