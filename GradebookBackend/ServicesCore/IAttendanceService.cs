using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface IAttendanceService
    {
        public SingleDayAttendancesListDTO GetAttendancesByStudentId(int studentId, int day, int month, int year);
        public SingleLessonAttendancesListDTO GetClassAttendances(int teacherId, int classId, int lessonId, int day, int month, int year);
    }
}
