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
using A_ZAcademy.Models.ViewModels;

namespace A_ZAcademy.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class InstructorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public InstructorsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Administrator/Instructors
        public async Task<IActionResult> Index()
        {
              return _context.instructors != null ? 
                          View(await _context.instructors.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.instructors'  is null.");
        }

        // GET: Administrator/Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.instructors == null)
            {
                return NotFound();
            }

            var instructor = await _context.instructors
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Administrator/Instructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstructorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? imgName = FileUpload(model);
                Instructor instructor = new Instructor
                {
                    InstructorId = model.InstructorId,
                    CreationDate=model.CreationDate,
                    Fb=model.Fb,
                    InstructorName=model.InstructorName,
                    IsDeleted=model.IsDeleted,
                    IsPublished=model.IsPublished,
                    LinkedIN=model.LinkedIN,
                    Position=model.Position,
                    Tw=model.Tw,
                    UserId=model.UserId,
                    InstructorImg=imgName
                };
                
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Administrator/Instructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.instructors == null)
            {
                return NotFound();
            }

            var instructor = await _context.instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        // POST: Administrator/Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InstructorId,InstructorName,InstructorImg,Position,Fb,Tw,LinkedIN,CreationDate,IsPublished,IsDeleted,UserId")] Instructor instructor)
        {
            if (id != instructor.InstructorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstructorExists(instructor.InstructorId))
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
            return View(instructor);
        }

        // GET: Administrator/Instructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.instructors == null)
            {
                return NotFound();
            }

            var instructor = await _context.instructors
                .FirstOrDefaultAsync(m => m.InstructorId == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Administrator/Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.instructors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.instructors'  is null.");
            }
            var instructor = await _context.instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.instructors.Remove(instructor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
          return (_context.instructors?.Any(e => e.InstructorId == id)).GetValueOrDefault();
        }
        public string FileUpload(InstructorViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.InstructorImg!.FileName);
            string newImgName = "A-z_" + fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.InstructorImg!.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.InstructorImg.CopyTo(fileStream);
            }

            return "\\img\\" + newImgName;
        }
    }
}
