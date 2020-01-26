using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IAttendanceStatusService
    {
        void AddAttendanceStatus(string newAttendanceStatusName);
        AttendanceStatusListDTO GetAttendanceStatusList();
    }
}
