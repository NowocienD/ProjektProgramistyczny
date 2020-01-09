using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class NewAttendanceDTO
    {
        public string Date { get; set; }
        public int LessonNumber { get; set; }
        public int AttendanceStatusId { get; set; }
    }
}
