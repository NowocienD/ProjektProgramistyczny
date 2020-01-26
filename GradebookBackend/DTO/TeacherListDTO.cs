using System.Collections.Generic;

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
