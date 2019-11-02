//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using StudentExecisesAPI.Models;
//namespace StudentExecisesAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CohortController : ControllerBase
//    {
//        private readonly IConfiguration _config;

//        //constructor
//        public CohortController(IConfiguration config)
//        {
//            _config = config;
//        }

//        public SqlConnection Connection
//        {
//            get
//            {
//                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
//            }
//        }
//        // Get all Students
//        [HttpGet]
//        public async Task<IActionResult> GetAllInstructors()
//        {
//            using (SqlConnection conn = Connection)
//            {
//                conn.Open();
//                using (SqlCommand cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"SELECT i.Id, i.FirstName, i.LastName, i.SlackHandle, 
//                                   i.CohortId, CohortName, i.Speciality
                                   
//                              FROM Instructor i INNER JOIN Cohort c ON i.CohortId = c.id";



//                    SqlDataReader reader = cmd.ExecuteReader();

//                    List<Cohort> cohorts = new List<Cohort>();

//                    while (reader.Read())
//                    {
//                        Cohort cohort = new Cohort()
//                        {
//                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
//                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
//                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
//                            SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
//                            Speciality = reader.GetString(reader.GetOrdinal("Speciality")),
//                            CohortId = reader.GetInt32(reader.GetOrdinal("CohortId")),
//                            Cohort = new Cohort()
//                            {
//                                Id = reader.GetInt32(reader.GetOrdinal("CohortId")),
//                                CohortName = reader.GetString(reader.GetOrdinal("CohortName")),
//                            }
//                        };

//                        cohorts.Add(cohort);

//                    }

//                    reader.Close();

//                    return Ok(cohorts);
//                }
//            }
//        }
//    }
//}
