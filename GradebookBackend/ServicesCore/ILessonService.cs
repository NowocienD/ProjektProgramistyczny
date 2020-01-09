using GradebookBackend.DTO;
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
        public int GetLessonId(int lessonNumber, int dayOfTheWeek, int classId);
    }
}
