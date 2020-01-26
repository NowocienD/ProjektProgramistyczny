using GradebookBackend.DTO;
using System;

namespace GradebookBackend.ServicesCore
{
    public interface IAttendanceService
    {
        public SingleDayAttendancesListDTO GetAttendancesByStudentId(int studentId, DateTime firstDate);
        public SingleLessonAttendancesListDTO GetClassAttendances(int classId, int lessonId, DateTime date);
        public void AddUpdateDeleteAttendance(DateTime date, int attendanceStatusId, int lessonId, int studentId);
    }
}
