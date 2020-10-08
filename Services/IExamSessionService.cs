using Examio.Dto;
using Examio.Models;
using Examio.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examio.Controllers
{
    public interface IExamSessionService
    {
        public Task<ExamSessionFormVM> ReturnBlankExamSessionForm ();

        public Task<ExamSessionFormVM> ReturnPopulatedExamSessionFormDto(
            ExamSessionDto examSessionDto);

        public Task<ExamSessionAllOrSearchedListVM> ExamSessionsAllOrSearchedListVM(
            ExamSessionSearchFilterDto examSessionSearchFilterDto);

        public Task<ExamSession> FindExamSessionById(int id);

        public Task<ExamSessionFormVM> ReturnPopulatedExamSessionForm(
            ExamSession examSession);

        public Task DeleteExamSessionById(int id);

        public void SaveNewExamSession(ExamSessionDto examSessionDto);

        public Task<bool> UpdateExamSession(ExamSessionDto examSessionDto);
    }
}