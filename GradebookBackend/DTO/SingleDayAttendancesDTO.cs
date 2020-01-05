using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class SingleDayAttendances
    {
        private readonly int maxNumberOfLessonForDay = 8;

        public string Date { get; set; }
        public int DayOfTheWeek { get; set; }
        public List<string> Attendances { get; set; }

        public SingleDayAttendance()
        {
            Attendances = new List<string>();
            for(int i = 0; i < maxNumberOfLessonForDay; i++)
            {
                Attendances.Add("Not entered");
            }
        }
    }
}
