using System.Collections.Generic;

namespace GradebookBackend.DTO
{
    public class AttendanceStatusListDTO
    {
        public List<AttendanceStatusDTO> AttendanceStatusDTOs { get; set; }
        public AttendanceStatusListDTO()
        {
            AttendanceStatusDTOs = new List<AttendanceStatusDTO>();
        }
    }
}
