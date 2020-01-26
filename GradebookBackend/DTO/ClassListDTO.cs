using System.Collections.Generic;

namespace GradebookBackend.DTO
{
    public class ClassListDTO
    {
        public List<ClassDTO> ClassList { get; set; }

        public ClassListDTO()
        {
            ClassList = new List<ClassDTO>();
        }
    }
}
