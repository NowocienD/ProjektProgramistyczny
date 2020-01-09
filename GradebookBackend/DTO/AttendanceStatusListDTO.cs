using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
