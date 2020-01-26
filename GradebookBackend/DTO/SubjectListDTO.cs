using System.Collections.Generic;

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
