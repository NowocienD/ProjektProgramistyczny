using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class Lesson
    {
        public int ID { get; set; }
        public int LessonNumber { get; set; }
        public int DayOfTheWeek { get; set; }

        public List<Attendance> Attendances { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
