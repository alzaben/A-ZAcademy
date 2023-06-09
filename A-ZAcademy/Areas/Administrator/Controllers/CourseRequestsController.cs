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
    public class CourseRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/CourseRequests
        public async Task<IActionResult> Index()
        {
              return _context.coursesRequests != null ? 
                          View(await _context.coursesRequests.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.coursesRequests'  is null.");
        }

        // GET: Administrator/CourseRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.coursesRequests == null)
            {
                return NotFound();
            }

            var courseRequest = await _context.coursesRequests
                .FirstOrDefaultAsync(m => m.CourseRequestID == id);
            if (courseRequest == null)
            {
                return NotFound();
            }

            return View(courseRequest);
        }

        // GET: Administrator/CourseRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/CourseRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseRequestID,Name,Email,Phone")] CourseRequest courseRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseRequest);
        }

        // GET: Administrator/CourseRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.coursesRequests == null)
            {
                return NotFound();
            }

            var courseRequest = await _context.coursesRequests.FindAsync(id);
            if (courseRequest == null)
            {
                return NotFound();
            }
            return View(courseRequest);
        }

        // POST: Administrator/CourseRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseRequestID,Name,Email,Phone")] CourseRequest courseRequest)
        {
            if (id != courseRequest.CourseRequestID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseRequestExists(courseRequest.CourseRequestID))
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
            return View(courseRequest);
        }

        // GET: Administrator/CourseRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.coursesRequests == null)
            {
                return NotFound();
            }

            var courseRequest = await _context.coursesRequests
                .FirstOrDefaultAsync(m => m.CourseRequestID == id);
            if (courseRequest == null)
            {
                return NotFound();
            }

            return View(courseRequest);
        }

        // POST: Administrator/CourseRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.coursesRequests == null)
            {
                return Problem("Entity set 'ApplicationDbContext.coursesRequests'  is null.");
            }
            var courseRequest = await _context.coursesRequests.FindAsync(id);
            if (courseRequest != null)
            {
                _context.coursesRequests.Remove(courseRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseRequestExists(int id)
        {
          return (_context.coursesRequests?.Any(e => e.CourseRequestID == id)).GetValueOrDefault();
        }
    }
}
