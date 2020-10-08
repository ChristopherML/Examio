using System;
using System.ComponentModel.DataAnnotations;
using static Examio.Validators.ExamSessionValidator;

namespace Examio.Dto
{
    public class ExamSessionDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        [EndStartPairingRequired]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        [EndStartPairingRequired]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Exam Site")]
        public int ExamSiteId { get; set; }

    }
}
