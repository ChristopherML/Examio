using Examio.Controllers;
using Examio.Data;
using Examio.Dto;
using Examio.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examio.Models.Services
{
    public class ExamSessionService : IExamSessionService
    {
        private readonly ExamioContext _context;

        public ExamSessionService(ExamioContext context)
        {
            _context = context;
        }

        public async Task<ExamSessionFormVM> ReturnBlankExamSessionForm()
        {
            var examSites = await _context.ExamSites.ToListAsync();
            var examSessionFormVM = new ExamSessionFormVM
            {
                ExamSites = examSites
            };

            return examSessionFormVM;
        }

        public async Task<ExamSessionFormVM> ReturnPopulatedExamSessionFormDto(
            ExamSessionDto examSessionDto)
        {
            var examSites = await _context.ExamSites.ToListAsync();
            var examSessionFormVM = new ExamSessionFormVM
            {
                ExamSessionDto = examSessionDto,
                ExamSites = examSites
            };

            return examSessionFormVM;
        }

        public async Task<ExamSession> FindExamSessionById(int id)
        {
            var examSessionEager = await _context.ExamSessions
                    .Include(e => e.ExamSite)
                    .FirstOrDefaultAsync(m => m.Id == id);

            return examSessionEager;
        }

        private async Task<ExamSession> MapFromDto(ExamSessionDto examSessionDto)
        {
            var examSite = await _context.ExamSites
                .FirstOrDefaultAsync(e => e.Id == examSessionDto.ExamSiteId);

            if (examSite != null)
            {
                var examSession = new ExamSession
                {
                    Id = examSessionDto.Id,
                    Description = examSessionDto.Description,
                    Name = examSessionDto.Name,
                    EndDate = examSessionDto.EndDate,
                    StartDate = examSessionDto.StartDate,
                    ExamSite = examSite
                };
                return examSession;
            }
            return null;
        }

        public async void SaveNewExamSession(ExamSessionDto examSessionDto)
        {
            _context.Add(await MapFromDto(examSessionDto));
            await _context.SaveChangesAsync();
        }

        private ExamSessionDto MapToDto(ExamSession examSession)
        {
            var examSessionDto = new ExamSessionDto
            {
                Id = examSession.Id,
                Description = examSession.Description,
                Name = examSession.Name,
                EndDate = examSession.EndDate,
                StartDate = examSession.StartDate,
                ExamSiteId = examSession.ExamSite.Id
            };
            return examSessionDto;
        }

        public Task<ExamSessionFormVM> ReturnPopulatedExamSessionForm(ExamSession examSession)
        {
            var examSessionDto = MapToDto(examSession);
            return ReturnPopulatedExamSessionFormDto(examSessionDto);
        }

        public async Task DeleteExamSessionById(int id)
        {
            var examSession = await FindExamSessionById(id);
            _context.ExamSessions.Remove(examSession);
            await _context.SaveChangesAsync();
        }

        private bool ExamSessionExists(int id)
        {
            return _context.ExamSessions.Any(e => e.Id == id);
        }

        public async Task<bool> UpdateExamSession(ExamSessionDto examSessionDto)
        {
            var examSession = await MapFromDto(examSessionDto);
            if (examSession == null)
            {
                return false;
            }
            try
            {
                _context.Update(examSession);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExamSessionExists(examSessionDto.Id))
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

        private async Task<List<ExamSession>> Search(
            ExamSessionSearchFilterDto search)
        {
            var examSessions = from e in _context.ExamSessions 
                                select e;
            var examSites = from e in _context.ExamSessions
                                select e;

            examSessions = examSessions.Where(e =>
            (string.IsNullOrEmpty(search.Name) || e.Name.Contains(search.Name)) &&
            (search.StartDate == null || (e.StartDate > search.StartDate)) &&
            (search.EndDate == null || (e.EndDate < search.EndDate)) &&
            (string.IsNullOrEmpty(search.ExamSiteName) || e.ExamSite.Name.Contains(search.ExamSiteName)));

            examSessions = examSessions.OrderBy(e => e.Name.ToLower());

            return await examSessions.Include(e => e.ExamSite).ToListAsync();
        }

        public async Task<ExamSessionAllOrSearchedListVM> ExamSessionsAllOrSearchedListVM(ExamSessionSearchFilterDto search)
        {
            var examSessionAllOrSearchFilteredListVM = new ExamSessionAllOrSearchedListVM
            {
                ExamSessions = await Search(search),
                Search = search
            };

            return examSessionAllOrSearchFilteredListVM;
        }
    }
}
