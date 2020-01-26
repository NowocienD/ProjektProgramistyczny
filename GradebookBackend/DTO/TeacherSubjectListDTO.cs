using System.Collections.Generic;

namespace GradebookBackend.DTO
{
    public class TeacherSubjectListDTO
    {
        public List<TeacherSubjectDTO> teacherSubjects { get; set; }
        public TeacherSubjectListDTO()
        {
            teacherSubjects = new List<TeacherSubjectDTO>();
        }
    }
}
