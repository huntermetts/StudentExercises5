using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesAPI.Models
{
    public class Cohort
    {
        public int id { get; set; }

        public string cohortName { get; set; }

        public List<Students> students { get; set; } = new List<Students>();

        public List <Instructors> instructors { get; set; } = new List<Instructors>();
    }
}
