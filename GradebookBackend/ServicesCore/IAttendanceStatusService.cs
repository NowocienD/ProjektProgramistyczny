using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface IAttendanceStatusService
    {
        public void AddAttendanceStatus(string newAttendanceStatusName);
        public AttendanceStatusListDTO GetAttendanceStatusList();
    }
}
