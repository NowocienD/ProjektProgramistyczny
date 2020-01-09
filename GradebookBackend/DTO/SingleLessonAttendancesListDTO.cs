using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class SingleLessonAttendancesListDTO
    {
        public List<SingleLessonAttendanceDTO> SingleLessonAttendances { get; set; }
        public SingleLessonAttendancesListDTO()
        {
            SingleLessonAttendances = new List<SingleLessonAttendanceDTO>();
        }

    }
}
