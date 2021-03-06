﻿using System.Collections.Generic;

namespace GradebookBackend.DTO
{
    public class SingleDayLessonPlanExtendedDTO
    {
        private readonly int maxNumberOfLessonForDay = 8;
        public List<LessonDTO> Lessons { get; set; }

        public SingleDayLessonPlanExtendedDTO()
        {
            Lessons = new List<LessonDTO>();
            for (int i = 0; i < maxNumberOfLessonForDay; i++)
            {
                Lessons.Add(new LessonDTO
                {
                    Name = "brak lekcji"
                });
            }
        }
    }
}
