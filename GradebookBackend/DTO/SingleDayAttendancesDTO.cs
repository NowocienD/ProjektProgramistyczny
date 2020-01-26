using System.Collections.Generic;

namespace GradebookBackend.DTO
{
    public class SingleDayAttendancesDTO
    {
        public List<string> Attendances { get; set; }

        public SingleDayAttendancesDTO()
        {
            Attendances = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                Attendances.Add("nie wpisano");
            }
        }
    }
}
