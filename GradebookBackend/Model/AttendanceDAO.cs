using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class AttendanceDAO
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string State { get; set; }

        public int LessonId { get; set; }
        public LessonDAO Lesson { get; set; }
    }
}
