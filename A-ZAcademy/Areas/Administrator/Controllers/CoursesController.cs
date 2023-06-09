using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using A_ZAcademy.Data;
using A_ZAcademy.Models;
using A_ZAcademy.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace A_ZAcademy.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles ="Admin")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public CoursesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult CountCourse()
        {
            int courseCount = _context.courses.Count();
            return View(courseCount);
        }


        // GET: Administrator/Courses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.courses.Include(c => c.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administrator/Courses/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(course);
        }

        // GET: Administrator/Courses/Create
        public  IActionResult Create()
        {


            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Administrator/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {


                string imgName = FileUpload(model);
                Course course = new Course
                {
                    Category = model.Category,
                    CategoryId = model.CategoryId,
                    CourseDescription = model.CourseDescription,
                    CourseId = model.CourseId,
                    CourseTitle = model.CourseTitle,
                    CreationDate = model.CreationDate,
                    Duration = model.Duration,
                    Hours = model.Hours,
                    IsDeleted = model.IsDeleted,
                    IsPublished = model.IsPublished,
                    Price = model.Price,
                    Rate = model.Rate,
                    StartDate = model.StartDate,
                    StartTime = model.StartTime,
                    StudentNumber = model.StudentNumber,
                    UserId = model.UserId,
                    Img=imgName
                   
                };

                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryID", "CategoryName", model.CategoryId);
            return View(model);
        }

        // GET: Administrator/Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.courses == null)
            {
                return NotFound();
            }

            var course = await _context.courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryID", "CategoryName", course.CategoryId);
            return View(course);
        }

        // POST: Administrator/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseTitle,CourseDescription,Price,Img,StartDate,StartTime,Duration,Hours,Rate,StudentNumber,CategoryId,CreationDate,IsPublished,IsDeleted,UserId")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.categories, "CategoryID", "CategoryName", course.CategoryId);
            return View(course);
        }

        // GET: Administrator/Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(course);
        }

        // POST: Administrator/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.courses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.courses'  is null.");
            }
            var course = await _context.courses.FindAsync(id);
            if (course != null)
            {
                _context.courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return (_context.courses?.Any(e => e.CourseId == id)).GetValueOrDefault();
        }
        public string FileUpload(CourseViewModel model)
        {
            string wwwPath = _webHostEnvironment.WebRootPath;
            if (string.IsNullOrEmpty(wwwPath)) { }
            string contentPath = _webHostEnvironment.ContentRootPath;
            if (string.IsNullOrEmpty(contentPath)) { }
            string p = Path.Combine(wwwPath, "img");
            if (!Directory.Exists(p))
            {
                Directory.CreateDirectory(p);
            }
            string fileName = Path.GetFileNameWithoutExtension(model.Img!.FileName);
            string newImgName = "A-z_" + fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.Img!.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.Img.CopyTo(fileStream);
            }

            return "\\img\\" + newImgName;
        }
        public IActionResult Count()
        {
            int recordCount = _context.courses.Count();
            return View(recordCount);
        }

    }
}
