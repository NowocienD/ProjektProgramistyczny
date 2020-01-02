using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
