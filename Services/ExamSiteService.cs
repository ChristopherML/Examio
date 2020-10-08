using Examio.Data;
using Examio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examio.Services
{
    public class ExamSiteService : IExamSiteService
    {
        private readonly ExamioContext _context;

        public ExamSiteService(ExamioContext context)
        {
            _context = context;
        }

        public async Task DeleteExamSite(int id)
        {
            var examSite = await _context.ExamSites.FindAsync(id);
            _context.ExamSites.Remove(examSite);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExamSite>> ExamSiteSortedList()
        {
            var examSites = from e in _context.ExamSites
                            select e;

            examSites = examSites.OrderBy(e => e.Name.ToLower());

            return await examSites.ToListAsync();
        }

        public async Task<ExamSite> FindExamSiteById(int id)
        {
            return await _context.ExamSites.FindAsync(id);
        }

        public async Task SaveExamSite(ExamSite examSite)
        {
            _context.Add(examSite);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateExamSite(ExamSite examSite)
        {
            try
            {
                _context.Update(examSite);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamSiteExists(examSite.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        private bool ExamSiteExists(int id)
        {
            return _context.ExamSites.Any(e => e.Id == id);
        }
    }
}
