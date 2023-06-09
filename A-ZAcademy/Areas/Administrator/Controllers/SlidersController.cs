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
    public class SlidersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public SlidersController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;   
        }

        // GET: Administrator/Sliders
        public async Task<IActionResult> Index()
        {
              return _context.sliders != null ? 
                          View(await _context.sliders.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.sliders'  is null.");
        }

        // GET: Administrator/Sliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.sliders
                .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Administrator/Sliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imgName = FileUpload(model);
                Slider slider = new Slider
                {
                    SliderId = model.SliderId,
                    SliderSubTitle = model.SliderSubTitle,
                    SliderTitle = model.SliderTitle,
                    CreationDate = model.CreationDate,
                    IsDeleted = model.IsDeleted,
                    IsPublished = model.IsPublished,
                    TxtLink = model.TxtLink,
                    UserId=model.UserId,
                    SliderImg = imgName

                };
                _context.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Administrator/Sliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }

        // POST: Administrator/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SliderId,SliderTitle,SliderSubTitle,TxtLink,SliderImg,CreationDate,IsPublished,IsDeleted,UserId")] Slider slider)
        {
            if (id != slider.SliderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.SliderId))
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
            return View(slider);
        }

        // GET: Administrator/Sliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.sliders
                .FirstOrDefaultAsync(m => m.SliderId == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // POST: Administrator/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.sliders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.sliders'  is null.");
            }
            var slider = await _context.sliders.FindAsync(id);
            if (slider != null)
            {
                _context.sliders.Remove(slider);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
          return (_context.sliders?.Any(e => e.SliderId == id)).GetValueOrDefault();
        }
        public string FileUpload(SliderViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.SliderImg!.FileName);
            string newImgName = "A-z_" + fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.SliderImg!.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.SliderImg.CopyTo(fileStream);
            }

            return "\\img\\" + newImgName;
        }
    }
}
