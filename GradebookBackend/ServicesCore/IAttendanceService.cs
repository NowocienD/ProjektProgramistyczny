using GradebookBackend.DTO;
using System;

namespace GradebookBackend.ServicesCore
{
    public interface IAttendanceService
    {
        SingleDayAttendancesListDTO GetAttendancesByStudentId(int studentId, DateTime firstDate);
        SingleLessonAttendancesListDTO GetClassAttendances(int classId, int lessonId, DateTime date);
        void AddUpdateDeleteAttendance(DateTime date, int attendanceStatusId, int lessonId, int studentId);
    }
}
