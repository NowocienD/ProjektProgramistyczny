using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface ILessonService
    {
        LessonPlanDTO GetStudentLessonPlanByClassId(int classId);
        LessonPlanDTO GetTeacherLessonPlanByTeacherId(int teacherId);
        SingleDayLessonPlanExtendedDTO GetSingleDayLessonPlanByDayOfTheWeekAndClassId(int dayOfTheWeek, int classId);
        int GetLessonId(int lessonNumber, int dayOfTheWeek, int classId);
        bool CheckIfLessonExists(int lessonNumber, int dayOfTheWeek, int classId);
        void AddLesson(NewLessonDTO newLessonDTO);
        void UpdateLesson(NewLessonDTO updatedLessonDTO, int lessonId);
        void DeleteLesson(int lessonId);
        LessonDTO GetLessonByLessonId(int lessonId);
    }
}
