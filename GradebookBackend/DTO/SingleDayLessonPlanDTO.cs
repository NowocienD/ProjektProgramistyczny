using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class SingleDayLessonPlanDTO
    {
        private readonly int maxNumberOfLessonForDay = 8;
        public List<string> Lessons { get; set; }

        public SingleDayLessonPlanDTO()
        {
            Lessons = new List<string>();
            for (int i = 0; i < maxNumberOfLessonForDay; i++)
            {
                Lessons.Add("brak lekcji");
            }
        }
    }
}
