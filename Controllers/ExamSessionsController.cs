using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examio.Data;
using Examio.Models;
using Examio.ViewModels;

namespace Examio.Controllers
{
    public class ExamSessionsController : Controller
    {
        private readonly ExamioContext _context;

        public ExamSessionsController(ExamioContext context)
        {
            _context = context;
        }

        // GET: ExamSessions
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExamSessions.ToListAsync());
        }

        // GET: ExamSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examSession = await _context.ExamSessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examSession == null)
            {
                return NotFound();
            }

            return View(examSession);
        }

        // GET: ExamSessions/Create
        public async Task<IActionResult> Create()
        {
            var examSites = await _context.ExamSites.ToListAsync();
            var examSessionFormDisplay = new ExamSessionFormVM
            {
                ExamSites = examSites
            };

            return View(examSessionFormDisplay);
        }

        // POST: ExamSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> 
            Create([Bind("Id,Name,Description,StartDate,EndDate,ExamSiteId")] ExamSessionFormVM examSessionFormVM)
        {
            var examSite = await _context.ExamSites.FirstOrDefaultAsync(e=> e.Id == examSessionFormVM.ExamSiteId);

            if (ModelState.IsValid && examSite != null)
            {
                var examSession = new ExamSession
                {
                    Id = examSessionFormVM.Id,
                    Description = examSessionFormVM.Description,
                    Name = examSessionFormVM.Name,
                    EndDate = examSessionFormVM.EndDate,
                    StartDate = examSessionFormVM.StartDate,
                    ExamSite = examSite
                };

                _context.Add(examSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(examSessionFormVM);
        }

        // GET: ExamSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examSession = await _context.ExamSessions.FindAsync(id);
            if (examSession == null)
            {
                return NotFound();
            }
            return View(examSession);
        }

        // POST: ExamSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,StartDate,EndDate")] ExamSession examSession)
        {
            if (id != examSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamSessionExists(examSession.Id))
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
            return View(examSession);
        }

        // GET: ExamSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examSession = await _context.ExamSessions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examSession == null)
            {
                return NotFound();
            }

            return View(examSession);
        }

        // POST: ExamSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examSession = await _context.ExamSessions.FindAsync(id);
            _context.ExamSessions.Remove(examSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamSessionExists(int id)
        {
            return _context.ExamSessions.Any(e => e.Id == id);
        }
    }
}
