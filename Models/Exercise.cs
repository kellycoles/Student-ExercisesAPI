using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExecisesAPI.Models
{
    public class Exercise
    {
        public int Id { get; set; }
        [Required]
        public string ExerciseName { get; set; }
        [Required]
        public string ExerciseLanguage { get; set; }
    }
}

