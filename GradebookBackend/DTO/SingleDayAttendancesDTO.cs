using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class SingleDayAttendancesDTO
    {
        public List<string> Attendances { get; set; }

        public SingleDayAttendancesDTO()
        {
            Attendances = new List<string>();
            for(int i = 0; i < 8; i++)
            {
                Attendances.Add("Not entered");
            }
        }
    }
}
