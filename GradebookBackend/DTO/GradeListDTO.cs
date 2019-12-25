using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class GradeListDTO
    {
        public List<GradeDTO> GradeDTOs { get; set; }
        public GradeListDTO()
        {
            GradeDTOs = new List<GradeDTO>(); 
        }
    }
}
