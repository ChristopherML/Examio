using Examio.Models.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace Examio.Models
{
    public class ExamSession
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [RequiredIf(new string[] { "EndDate,!null" })]
        public DateTime StartDate { get; set; }

        [RequiredIf(new string[] { "StartDate,!null" })]
        public DateTime EndDate { get; set; }

        [Required]  
        public ExamSite Site { get; set; }
    }
}
