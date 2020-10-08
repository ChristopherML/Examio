using Examio.Dto;
using System.ComponentModel.DataAnnotations;

namespace Examio.Validators
{
    public class ExamSessionValidator
    {
        public class EndStartPairingRequired : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var examSessionDto = (ExamSessionDto)validationContext.ObjectInstance;
                
                if (((examSessionDto.StartDate != null) && (examSessionDto.EndDate != null)) || ((examSessionDto.StartDate == null) && (examSessionDto.EndDate == null)))
                {
                    return ValidationResult.Success;
                }
                else
                {
                   return new ValidationResult("If set, both a start date and an end date are required");
                }
            }
        }
    }
}
