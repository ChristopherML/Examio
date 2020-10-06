using Examio.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Examio.ViewModels
{
    public class ExamSessionFormVM
    {
        public IEnumerable<ExamSite> ExamSites { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name= "Exam Site")]
        public int ExamSiteId { get; set; }
    }
}
