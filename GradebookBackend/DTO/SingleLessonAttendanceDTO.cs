using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class SingleLessonAttendanceDTO
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int AttendanceStatusId { get; set; }
        public string AttendanceStatus { get; set; }
    }
}
