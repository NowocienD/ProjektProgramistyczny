using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System.Collections.Generic;

namespace GradebookBackend.Services
{
    public class AttendanceStatusService : IAttendanceStatusService
    {
        private readonly IRepository<AttendanceStatusDAO> attendanceStatusRepository;

        public AttendanceStatusService(IRepository<AttendanceStatusDAO> attendanceStateRepository)
        {
            this.attendanceStatusRepository = attendanceStateRepository;
        }

        public void AddAttendanceStatus(string newAttendanceStatusName)
        {
            attendanceStatusRepository.Add(new AttendanceStatusDAO
            {
                Name = newAttendanceStatusName
            });
        }
        public AttendanceStatusListDTO GetAttendanceStatusList()
        {
            AttendanceStatusListDTO attendanceStatusListDTO = new AttendanceStatusListDTO();
            IEnumerable<AttendanceStatusDAO> attendancesStatus = attendanceStatusRepository.GetAll();
            foreach (AttendanceStatusDAO attendanceStatus in attendancesStatus)
            {
                attendanceStatusListDTO.AttendanceStatusDTOs.Add(new AttendanceStatusDTO
                {
                    Id = attendanceStatus.Id,
                    Name = attendanceStatus.Name
                });
            }
            return attendanceStatusListDTO;
        }
    }
}
