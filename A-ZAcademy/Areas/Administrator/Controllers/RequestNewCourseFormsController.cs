using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using A_ZAcademy.Data;
using A_ZAcademy.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace A_ZAcademy.Areas.Administrator.Controllers
{
    [Area("Administrator")]
	[Authorize(Roles = "Admin")]
	public class RequestNewCourseFormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestNewCourseFormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/RequestNewCourseForms
        public async Task<IActionResult> Index()
        {
              return _context.requestNewCoursesForm != null ? 
                          View(await _context.requestNewCoursesForm.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.requestNewCoursesForm'  is null.");
        }

        // GET: Administrator/RequestNewCourseForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.requestNewCoursesForm == null)
            {
                return NotFound();
            }

            var requestNewCourseForm = await _context.requestNewCoursesForm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestNewCourseForm == null)
            {
                return NotFound();
            }

            return View(requestNewCourseForm);
        }

        // GET: Administrator/RequestNewCourseForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/RequestNewCourseForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone,CourseName,CourseDescription,ExpectedPrice,CreationDate,IsPublished,IsDeleted,UserId")] RequestNewCourseForm requestNewCourseForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestNewCourseForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(requestNewCourseForm);
        }

        // GET: Administrator/RequestNewCourseForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.requestNewCoursesForm == null)
            {
                return NotFound();
            }

            var requestNewCourseForm = await _context.requestNewCoursesForm.FindAsync(id);
            if (requestNewCourseForm == null)
            {
                return NotFound();
            }
            return View(requestNewCourseForm);
        }

        // POST: Administrator/RequestNewCourseForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone,CourseName,CourseDescription,ExpectedPrice,CreationDate,IsPublished,IsDeleted,UserId")] RequestNewCourseForm requestNewCourseForm)
        {
            if (id != requestNewCourseForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestNewCourseForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestNewCourseFormExists(requestNewCourseForm.Id))
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
            return View(requestNewCourseForm);
        }

        // GET: Administrator/RequestNewCourseForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.requestNewCoursesForm == null)
            {
                return NotFound();
            }

            var requestNewCourseForm = await _context.requestNewCoursesForm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestNewCourseForm == null)
            {
                return NotFound();
            }

            return View(requestNewCourseForm);
        }

        // POST: Administrator/RequestNewCourseForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.requestNewCoursesForm == null)
            {
                return Problem("Entity set 'ApplicationDbContext.requestNewCoursesForm'  is null.");
            }
            var requestNewCourseForm = await _context.requestNewCoursesForm.FindAsync(id);
            if (requestNewCourseForm != null)
            {
                _context.requestNewCoursesForm.Remove(requestNewCourseForm);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestNewCourseFormExists(int id)
        {
          return (_context.requestNewCoursesForm?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
