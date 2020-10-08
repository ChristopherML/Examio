using System;
using System.ComponentModel;

namespace Examio.Dto
{
    public class ExamSessionSearchFilterDto
    {
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string ExamSiteName { get; set; }

    }
}