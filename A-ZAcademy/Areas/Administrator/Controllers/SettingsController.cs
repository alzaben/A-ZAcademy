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
    public class SettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/Settings
        public async Task<IActionResult> Index()
        {
              return _context.settings != null ? 
                          View(await _context.settings.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.settings'  is null.");
        }

        // GET: Administrator/Settings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.settings == null)
            {
                return NotFound();
            }

            var setting = await _context.settings
                .FirstOrDefaultAsync(m => m.SettingId == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // GET: Administrator/Settings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Settings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SettingId,Email,Location,PhoneNumber,WebName,CreationDate,IsPublished,IsDeleted,UserId")] Setting setting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(setting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }

        // GET: Administrator/Settings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.settings == null)
            {
                return NotFound();
            }

            var setting = await _context.settings.FindAsync(id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }

        // POST: Administrator/Settings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SettingId,Email,Location,PhoneNumber,WebName,CreationDate,IsPublished,IsDeleted,UserId")] Setting setting)
        {
            if (id != setting.SettingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(setting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.SettingId))
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
            return View(setting);
        }

        // GET: Administrator/Settings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.settings == null)
            {
                return NotFound();
            }

            var setting = await _context.settings
                .FirstOrDefaultAsync(m => m.SettingId == id);
            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        // POST: Administrator/Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.settings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.settings'  is null.");
            }
            var setting = await _context.settings.FindAsync(id);
            if (setting != null)
            {
                _context.settings.Remove(setting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SettingExists(int id)
        {
          return (_context.settings?.Any(e => e.SettingId == id)).GetValueOrDefault();
        }
    }
}
