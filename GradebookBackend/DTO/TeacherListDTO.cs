using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class TeacherListDTO
    {
        public List<TeacherDTO> teachers { get; set; }
        public TeacherListDTO()
        {
            teachers = new List<TeacherDTO>();
        }
    }
}
