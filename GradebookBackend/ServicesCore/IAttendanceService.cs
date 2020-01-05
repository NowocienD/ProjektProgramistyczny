using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface IAttendanceService
    {
        public void GetAttendancesByStudentId(int studentId);
    }
}
