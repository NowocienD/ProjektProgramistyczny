using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
