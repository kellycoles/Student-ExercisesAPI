using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExecisesAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();

        //T his is to hold the actual foreign key integer
        public int CohortId { get; set; }

        // This property is for storing the C# object representing the department
        public Cohort Cohort { get; set; }
    }
}
