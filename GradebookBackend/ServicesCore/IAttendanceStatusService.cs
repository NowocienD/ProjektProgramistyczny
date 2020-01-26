using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IAttendanceStatusService
    {
        public void AddAttendanceStatus(string newAttendanceStatusName);
        public AttendanceStatusListDTO GetAttendanceStatusList();
    }
}
