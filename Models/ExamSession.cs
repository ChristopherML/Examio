﻿using Examio.Models.Validators;
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

        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]  
        public ExamSite ExamSite { get; set; }
    }
}
