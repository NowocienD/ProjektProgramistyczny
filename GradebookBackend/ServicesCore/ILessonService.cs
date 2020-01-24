﻿using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface ILessonService
    {
        public LessonPlanDTO GetStudentLessonPlanByClassId(int classId);
        public LessonPlanDTO GetTeacherLessonPlanByTeacherId(int teacherId);
        public SingleDayLessonPlanDTO GetSingleDayLessonPlanByDayOfTheWeekAndClassId(int dayOfTheWeek, int classId);
        public int GetLessonId(int lessonNumber, int dayOfTheWeek, int classId);
        public bool CheckIfLessonExists(int lessonNumber, int dayOfTheWeek, int classId);
        public void AddNewLesson(NewLessonDTO newLessonDTO);
        public void UpdateLesson(NewLessonDTO updatedLessonDTO, int lessonId);
    }
}
