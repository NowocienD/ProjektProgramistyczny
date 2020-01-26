using System.Collections.Generic;

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
