using System.Collections.Generic;

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
