using Examio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examio.Services
{
    public interface IExamSiteService
    {
        public Task<IEnumerable<ExamSite>> ExamSiteSortedList();

        public Task<ExamSite> FindExamSiteById(int id);

        public Task SaveExamSite(ExamSite examSite);

        public Task<bool> UpdateExamSite(ExamSite examSite);

        public Task DeleteExamSite(int id);
    }
}
