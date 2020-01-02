using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class ClassListDTO
    {
        public List<ClassDTO> ClassDTOs { get; set; }

        public ClassListDTO()
        {
            ClassDTOs = new List<ClassDTO>();
        }
    }
}
