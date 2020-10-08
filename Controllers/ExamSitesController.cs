using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examio.Data;
using Examio.Models;
using Examio.Services;

namespace Examio.Controllers
{
    public class ExamSitesController : Controller
    {
        private readonly IExamSiteService _examSiteService;

        public ExamSitesController(IExamSiteService examSiteService)
        {
            _examSiteService = examSiteService;
        }

        // GET: ExamSites
        public async Task<IActionResult> Index()
        {
            return View(await _examSiteService.ExamSiteSortedList());
        }

        // GET: ExamSites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examSite = await _examSiteService.FindExamSiteById((int)id);
            if (examSite == null)
            {
                return NotFound();
            }

            return View(examSite);
        }

        // GET: ExamSites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExamSites/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name")] ExamSite examSite)
        {
            if (ModelState.IsValid)
            {
                await _examSiteService.SaveExamSite(examSite);
                return RedirectToAction(nameof(Index));
            }
            return View(examSite);
        }

        // GET: ExamSites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examSite = await _examSiteService.FindExamSiteById((int)id);
            if (examSite == null)
            {
                return NotFound();
            }
            return View(examSite);
        }

        // POST: ExamSites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name")] ExamSite examSite)
        {
            if (id != examSite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                if (await _examSiteService.UpdateExamSite(examSite))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }

            }
            return View(examSite);
        }

        // GET: ExamSites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examSite = await _examSiteService.FindExamSiteById((int)id);

            if (examSite == null)
            {
                return NotFound();
            }

            return View(examSite);
        }

        // POST: ExamSites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _examSiteService.DeleteExamSite(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
