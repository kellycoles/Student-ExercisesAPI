using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExecisesAPI.Models
{
    public class Cohort
    {
        public int Id { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 5)]
        public string CohortName { get; set; }
        public List<Instructor> Instructors{ get; set; } = new List<Instructor>();
        public List<Student> Students{ get; set; } = new List<Student>();
    }
}
