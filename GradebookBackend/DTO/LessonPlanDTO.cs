﻿using System.Collections.Generic;

namespace GradebookBackend.DTO
{
    public class LessonPlanDTO
    {
        public List<SingleDayLessonPlanDTO> LessonPlan { get; set; }

        public LessonPlanDTO()
        {
            LessonPlan = new List<SingleDayLessonPlanDTO>();
            for(int i = 0; i < 5; i++)
            {
                LessonPlan.Add(new SingleDayLessonPlanDTO());
            }
        }
    }
}
