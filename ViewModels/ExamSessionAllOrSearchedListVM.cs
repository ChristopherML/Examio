using Examio.Dto;
using Examio.Models;
using System.Collections.Generic;

namespace Examio.ViewModels
{
    public class ExamSessionAllOrSearchedListVM
    {
        public List<ExamSession> ExamSessions { get; set; }

        public ExamSessionSearchFilterDto Search { get; set; }
    }
}
