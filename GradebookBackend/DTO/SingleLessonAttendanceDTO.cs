using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class SingleLessonAttendanceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Attendance { get; set; }
    }
}
