using System.ComponentModel.DataAnnotations;

namespace Examio.Models
{
    public class ExamSessionDto : ExamSession
    {
        [Required]
        public new int ExamSite { get; set; }
    }
}