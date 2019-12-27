using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class LessonDAO
    {
        public int Id { get; set; }
        public int LessonNumber { get; set; }
        public int DayOfTheWeek { get; set; }

        public List<AttendanceDAO> Attendances { get; set; }

        public int SubjectId { get; set; }
        public SubjectDAO Subject { get; set; }

        public int ClassId { get; set; }
        public ClassDAO Class { get; set; }

        public int TeacherId { get; set; }
        public TeacherDAO Teacher { get; set; }
    }
}
