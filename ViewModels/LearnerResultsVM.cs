using Examio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Examio.ViewModels
{
    public class LearnerResultsVM
    {
        public class LearnerResult
        {
            public class Learner
            {
                public string FirstName { get; set; }

                public string LastName { get; set; }

                [Display(Name = "DOB")]
                [DataType(DataType.Date)]
                public DateTime BirthDate { get; set; }

                [Display(Name = "Ref")]
                public string Reference { get; set; }
            }

            public class LearnerAssesment
            {
                [Display(Name = "ref")]
                public string Reference { get; set; }

                public int Mark { get; set; }

                [Range(0, 3)]
                public int Grade { get; set; } /* Assuming zero indexed mapping in view to [ "Fail", "Pass", "Merit", "Distinction" ]*/
            }

            public IEnumerable<LearnerAssesment> LearnerAssesments { get; set; }
        }

        public IEnumerable<LearnerResult> LearnersResults { get; set; }

        public ExamSession ExamSession { get; set; }
    }




}
