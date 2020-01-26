using System.Collections.Generic;

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
