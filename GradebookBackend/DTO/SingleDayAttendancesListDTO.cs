using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class SingleDayAttendancesListDTO
    {
        List<SingleDayAttendancesDTO> AttendancesPlan;

        public SingleDayAttendancesListDTO()
        {
            AttendancesPlan = new List<SingleDayAttendancesDTO>();
        }
    }
}
