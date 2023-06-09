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
	public class RequestNewCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RequestNewCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/RequestNewCourses
        public async Task<IActionResult> Index()
        {
              return _context.requestNewCourses != null ? 
                          View(await _context.requestNewCourses.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.requestNewCourses'  is null.");
        }

        // GET: Administrator/RequestNewCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.requestNewCourses == null)
            {
                return NotFound();
            }

            var requestNewCourse = await _context.requestNewCourses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestNewCourse == null)
            {
                return NotFound();
            }

            return View(requestNewCourse);
        }

        // GET: Administrator/RequestNewCourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/RequestNewCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,RequestLink,CreationDate,IsPublished,IsDeleted,UserId")] RequestNewCourse requestNewCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestNewCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(requestNewCourse);
        }

        // GET: Administrator/RequestNewCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.requestNewCourses == null)
            {
                return NotFound();
            }

            var requestNewCourse = await _context.requestNewCourses.FindAsync(id);
            if (requestNewCourse == null)
            {
                return NotFound();
            }
            return View(requestNewCourse);
        }

        // POST: Administrator/RequestNewCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,RequestLink,CreationDate,IsPublished,IsDeleted,UserId")] RequestNewCourse requestNewCourse)
        {
            if (id != requestNewCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestNewCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestNewCourseExists(requestNewCourse.Id))
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
            return View(requestNewCourse);
        }

        // GET: Administrator/RequestNewCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.requestNewCourses == null)
            {
                return NotFound();
            }

            var requestNewCourse = await _context.requestNewCourses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestNewCourse == null)
            {
                return NotFound();
            }

            return View(requestNewCourse);
        }

        // POST: Administrator/RequestNewCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.requestNewCourses == null)
            {
                return Problem("Entity set 'ApplicationDbContext.requestNewCourses'  is null.");
            }
            var requestNewCourse = await _context.requestNewCourses.FindAsync(id);
            if (requestNewCourse != null)
            {
                _context.requestNewCourses.Remove(requestNewCourse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestNewCourseExists(int id)
        {
          return (_context.requestNewCourses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
