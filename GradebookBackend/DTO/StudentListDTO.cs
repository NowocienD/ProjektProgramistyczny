using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class StudentListDTO
    {
        public List<StudentDTO> studentList { get; set; }

        public StudentListDTO()
        {
            studentList = new List<StudentDTO>();
        }
    }
}
