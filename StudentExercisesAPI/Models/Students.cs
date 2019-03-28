﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentExercisesAPI.Models
{
    public class Students
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string SlackHandle { get; set; }

        [Required]
        public int CohortId { get; set; }

        public Cohort Cohort { get; set; }
        public List<Exercises> Exercises { get; set; } = new List<Exercises>();
    }
}
