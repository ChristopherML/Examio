using Examio.Dto;
using Examio.Models;
using System.Collections.Generic;

namespace Examio.ViewModels
{
    public class ExamSessionFormVM
    {
        public IEnumerable<ExamSite> ExamSites { get; set; }

        public ExamSessionDto ExamSessionDto { get; set; }
    }
}
