using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesAPI.Models;

namespace StudentExercisesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CohortController : ControllerBase
    {
       private readonly IConfiguration _config;

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

        /*
        [HttpGet]
        public List<Cohort> GetAllCohorts()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select student.id as StudentId, student.FirstName as StudentFirstName, student.LastName as StudentLastName, student.SlackHandle as StudentSlackHandle," +
                       " cohort.id as cohortId, cohort.[name] as cohortName, " +
                       "Instructor.id as InstructorsId, Instructor.FirstName as instructorFirstName, Instructor.LastName as instructorLastName, instructor.slackHandle as instructorSlackHandle from Student" +
                       " left join cohort on student.CohortId = cohort.id" +
                       " left join instructor on cohort.id = Instructor.CohortId";
                    SqlDataReader reader = cmd.ExecuteReader();
                    Dictionary<int, Cohort> cohorts = new Dictionary<int,Cohort>();

                    while (reader.Read())
                    {
                        int cohortId = reader.GetInt32(reader.GetOrdinal("CohortId"));
                        if (!cohorts.ContainsKey(cohortId))
                        {
                            Cohort newCohort = new Cohort
                            {
                                id = reader.GetInt32(reader.GetOrdinal("CohortId")),
                                cohortName = reader.GetString(reader.GetOrdinal("cohortName")),
                            };
                            cohorts.Add(cohortId, newCohort);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("StudentId")))
                        {
                            Cohort thisCohort = cohorts[cohortId];
                           thisCohort.students.Add(
                                new Students
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("StudentId")),
                                    FirstName = reader.GetString(reader.GetOrdinal("StudentFirstName")),
                                    LastName = reader.GetString(reader.GetOrdinal("StudentLastName")),
                                    SlackHandle = reader.GetString(reader.GetOrdinal("StudentSlackHandle")),
                                    CohortId = reader.GetInt32(reader.GetOrdinal("cohortId")),
                                    name = reader.GetString(reader.GetOrdinal("cohortName"))

                                }
                            );
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("InstructorsId")))
                        {
                            Cohort thisCohort = cohorts[cohortId];
                            thisCohort.instructors.Add(
                                 new Instructors
                                 {
                                     Id = reader.GetInt32(reader.GetOrdinal("InstructorsId")),
                                     FirstName = reader.GetString(reader.GetOrdinal("instructorFirstName")),
                                     LastName = reader.GetString(reader.GetOrdinal("instructorLastName")),
                                     SlackHandle = reader.GetString(reader.GetOrdinal("instructorSlackHandle")),
                                     CohortId = reader.GetInt32(reader.GetOrdinal("cohortId")),
                                     name = reader.GetString(reader.GetOrdinal("cohortName"))

                                 }
                             );
                        }
                    }
                    reader.Close();
                    return cohorts.Values.ToList();
                }
            }
        }

        [HttpGet("{id}", Name = "GetCohort")]
        public List<Cohort> GetCohorts([FromRoute] int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select student.id as StudentId, student.FirstName as StudentFirstName, student.LastName as StudentLastName, student.SlackHandle as StudentSlackHandle," +
                       " cohort.id as cohortId, cohort.[name] as cohortName, " +
                       "Instructor.id as InstructorsId, Instructor.FirstName as instructorFirstName, Instructor.LastName as instructorLastName, instructor.slackHandle as instructorSlackHandle from Student" +
                       " left join cohort on student.CohortId = cohort.id" +
                       " left join instructor on cohort.id = Instructor.CohortId" +
                       "WHERE cohort.id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();
                    Dictionary<int, Cohort> cohorts = new Dictionary<int, Cohort>();

                    while (reader.Read())
                    {
                        int cohortId = reader.GetInt32(reader.GetOrdinal("CohortId"));
                        if (!cohorts.ContainsKey(cohortId))
                        {
                            Cohort newCohort = new Cohort
                            {
                                id = reader.GetInt32(reader.GetOrdinal("CohortId")),
                                cohortName = reader.GetString(reader.GetOrdinal("cohortName")),
                            };
                            cohorts.Add(cohortId, newCohort);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("StudentId")))
                        {
                            Cohort thisCohort = cohorts[cohortId];
                            thisCohort.students.Add(
                                 new Students
                                 {
                                     Id = reader.GetInt32(reader.GetOrdinal("StudentId")),
                                     FirstName = reader.GetString(reader.GetOrdinal("StudentFirstName")),
                                     LastName = reader.GetString(reader.GetOrdinal("StudentLastName")),
                                     SlackHandle = reader.GetString(reader.GetOrdinal("StudentSlackHandle")),
                                     CohortId = reader.GetInt32(reader.GetOrdinal("cohortId")),
                                     name = reader.GetString(reader.GetOrdinal("cohortName"))

                                 }
                             );
                        }

                        if (!reader.IsDBNull(reader.GetOrdinal("InstructorsId")))
                        {
                            Cohort thisCohort = cohorts[cohortId];
                            thisCohort.instructors.Add(
                                 new Instructors
                                 {
                                     Id = reader.GetInt32(reader.GetOrdinal("InstructorsId")),
                                     FirstName = reader.GetString(reader.GetOrdinal("instructorFirstName")),
                                     LastName = reader.GetString(reader.GetOrdinal("instructorLastName")),
                                     SlackHandle = reader.GetString(reader.GetOrdinal("instructorSlackHandle")),
                                     CohortId = reader.GetInt32(reader.GetOrdinal("cohortId")),
                                     name = reader.GetString(reader.GetOrdinal("cohortName"))

                                 }
                             );
                        }
                    }
                    reader.Close();
                    return cohorts.Values.ToList();
                }
            }
        }

    */

    }
}
