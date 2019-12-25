using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class NoteListDTO
    {
        public List<NoteDTO> NoteDTOs { get; set; } = new List<NoteDTO>();
    }
}
