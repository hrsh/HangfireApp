using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HangfireApp;
using HangfireApp.Models;

namespace HangfireApp.Controllers
{
    public class EmailEntitiesController : Controller
    {
        private readonly MyDbContext _context;

        public EmailEntitiesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: EmailEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Emails.ToListAsync());
        }

        // GET: EmailEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailEntity = await _context.Emails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emailEntity == null)
            {
                return NotFound();
            }

            return View(emailEntity);
        }

        // GET: EmailEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmailEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Body")] EmailEntity emailEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emailEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emailEntity);
        }

        // GET: EmailEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailEntity = await _context.Emails.FindAsync(id);
            if (emailEntity == null)
            {
                return NotFound();
            }
            return View(emailEntity);
        }

        // POST: EmailEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Subject,Body")] EmailEntity emailEntity)
        {
            if (id != emailEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailEntityExists(emailEntity.Id))
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
            return View(emailEntity);
        }

        // GET: EmailEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailEntity = await _context.Emails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emailEntity == null)
            {
                return NotFound();
            }

            return View(emailEntity);
        }

        // POST: EmailEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emailEntity = await _context.Emails.FindAsync(id);
            _context.Emails.Remove(emailEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailEntityExists(int id)
        {
            return _context.Emails.Any(e => e.Id == id);
        }
    }
}
