using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcJav.Data;
using MvcJav.Models;

namespace MvcJav.Controllers
{
    public class JavsController : Controller
    {
        private readonly MvcJavContext _context;

        public JavsController(MvcJavContext context)
        {
            _context = context;
        }

      
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        // GET: Javs
        public async Task<IActionResult> Index(string javActor, string searchString)
        {
            if (_context.Jav == null)
            {
                return Problem("Entity set 'MvcJavContext.Jav'  is null.");
            }
            // Use LINQ to get list of actors.
            IQueryable<string> actorQuery = from j in _context.Jav
                                            orderby j.Actor
                                            select j.Actor;
            var javs = from j in _context.Jav
                       select j;

            if (!String.IsNullOrEmpty(searchString))
            {
                javs = javs.Where(s => s.Name!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(javActor))
            {
                javs = javs.Where(x => x.Actor == javActor);
            }

            var javActorVM = new JavActorViewModel
            {
                Actors = new SelectList(await actorQuery.Distinct().ToListAsync()),
                Javs = await javs.ToListAsync()
            };

            return View(javActorVM);
        }

        // GET: Javs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Jav == null)
            {
                return NotFound();
            }

            var jav = await _context.Jav
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jav == null)
            {
                return NotFound();
            }

            return View(jav);
        }

        // GET: Javs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Javs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Actor,Price,Rating,ReleaseDate")] Jav jav)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jav);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jav);
        }

        // GET: Javs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Jav == null)
            {
                return NotFound();
            }

            var jav = await _context.Jav.FindAsync(id);
            if (jav == null)
            {
                return NotFound();
            }
            return View(jav);
        }

        // POST: Javs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Actor,Price,Rating,ReleaseDate")] Jav jav)
        {
            if (id != jav.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jav);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JavExists(jav.Id))
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
            return View(jav);
        }

        // GET: Javs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Jav == null)
            {
                return NotFound();
            }

            var jav = await _context.Jav
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jav == null)
            {
                return NotFound();
            }

            return View(jav);
        }

        // POST: Javs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, bool notUsed)
        {
            if (_context.Jav == null)
            {
                return Problem("Entity set 'MvcJavContext.Jav'  is null.");
            }
            var jav = await _context.Jav.FindAsync(id);
            if (jav != null)
            {
                _context.Jav.Remove(jav);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JavExists(int id)
        {
          return (_context.Jav?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
