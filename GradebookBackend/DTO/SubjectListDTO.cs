using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class SubjectListDTO
    {
        public List<SubjectDTO> SubjectList { get; set; }

        public SubjectListDTO()
        {
            SubjectList = new List<SubjectDTO>();
        }
    }

}
