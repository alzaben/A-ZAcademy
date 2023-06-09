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
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public BlogsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment= webHostEnvironment;
        }

        // GET: Administrator/Blogs
        public async Task<IActionResult> Index()
        {
              return _context.blogs != null ? 
                          View(await _context.blogs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.blogs'  is null.");
        }

        // GET: Administrator/Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Administrator/Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? imgName = FileUpload(model);
                Blog blog = new Blog
                {
                    BlogDescription=model.BlogDescription,
                    BlogId=model.BlogId,
                    BlogTitle=model.BlogTitle,
                    CreationDate=model.CreationDate,
                    IsDeleted=model.IsDeleted,
                    IsPublished=model.IsPublished,
                    BlogImg=imgName,
                    UserId=model.UserId
                };
                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Administrator/Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Administrator/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,BlogTitle,BlogImg,BlogDescription,CreationDate,IsPublished,IsDeleted,UserId")] Blog blog)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
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
            return View(blog);
        }

        // GET: Administrator/Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Administrator/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.blogs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.blogs'  is null.");
            }
            var blog = await _context.blogs.FindAsync(id);
            if (blog != null)
            {
                _context.blogs.Remove(blog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
          return (_context.blogs?.Any(e => e.BlogId == id)).GetValueOrDefault();
        }
        public string FileUpload(BlogViewModel model)
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
            string fileName = Path.GetFileNameWithoutExtension(model.BlogImg!.FileName);
            string newImgName = "A-z_" + fileName + "_" + Guid.NewGuid().ToString() + Path.GetExtension(model.BlogImg!.FileName);
            using (FileStream fileStream = new FileStream(Path.Combine(p, newImgName), FileMode.Create))
            {
                model.BlogImg!.CopyTo(fileStream);
            }

            return "\\img\\" + newImgName;
        }
    }
}
