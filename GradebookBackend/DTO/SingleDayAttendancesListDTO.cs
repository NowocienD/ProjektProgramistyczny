using System.Collections.Generic;

namespace GradebookBackend.DTO
{
    public class SingleDayAttendancesListDTO
    {
        public List<SingleDayAttendancesDTO> AttendancesPlan { get; set; }

        public SingleDayAttendancesListDTO()
        {
            AttendancesPlan = new List<SingleDayAttendancesDTO>();
            for (int i = 0; i < 5; i++)
            {
                AttendancesPlan.Add(new SingleDayAttendancesDTO());
            }
        }
    }
}
