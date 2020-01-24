using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface IAttendanceService
    {
        public SingleDayAttendancesListDTO GetAttendancesByStudentId(int studentId, DateTime firstDate);
        public SingleLessonAttendancesListDTO GetClassAttendances(int classId, int lessonId, DateTime date);
        public void AddUpdateDeleteAttendance(DateTime date, int attendanceStatusId, int lessonId, int studentId);
    }
}
