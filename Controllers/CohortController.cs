using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExecisesAPI.Models;
namespace StudentExecisesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CohortController : ControllerBase
    {
        private readonly IConfiguration _config;

        //constructor
        public CohortController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // Get all Students
        [HttpGet]
        public async Task<IActionResult> GetAllInstructors()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id, c.CohortName, s.Id AS studentId, s.FirstName,s.LastName, s.SlackHandle, 
                                      s.CohortId,i.Id AS instructorId, i.FirstName, i.LastName, i.SlackHandle, 
                                      i.CohortId,i.SlackHandle, i.Speciality
                                      FROM Cohort c JOIN Student s ON s.CohortId = c.id
                                      Join Instructor ON  i.CohortId = c.id
                                      LEFT JOIN StudentExercise se on se.StudentId = s.id
                                      LEFT JOIN Exercise e on se.ExerciseId = e.Id";


                    SqlDataReader reader = cmd.ExecuteReader();

                    Dictionary<int, Cohort> cohorts = new Dictionary<int, Cohort>();

                    while (reader.Read())
                    {
                        int cohortId = reader.GetInt32(reader.GetOrdinal("Id"));
                        if (!cohorts.ContainsKey(cohortId))
                        {
                            Cohort cohort = new Cohort()
                            {

                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                CohortName = reader.GetString(reader.GetOrdinal("CohortName"))
                            };
                            cohorts.Add(cohortId, cohort);
                        }
                        Cohort fromDictionary = cohorts[cohortId];
                        if (!reader.IsDBNull(reader.GetOrdinal("studentId")))
                        {

                            Student student = new Student()
                            {

                                Id = reader.GetInt32(reader.GetOrdinal("studentId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                                CohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
                            };

                            fromDictionary.Students.Add(student);
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("InstructorId")))
                        {
                            Instructor instructor = new Instructor()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("instructorId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                                CohortId = reader.GetInt32(reader.GetOrdinal("CohortId")),
                            };

                            fromDictionary.Instructors.Add(instructor);
                        }

                    }

                    reader.Close();

                    return Ok(cohorts.Values);

                };

            }
         
        }
    }
}



