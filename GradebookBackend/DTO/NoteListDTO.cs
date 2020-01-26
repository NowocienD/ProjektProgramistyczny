using System.Collections.Generic;

namespace GradebookBackend.DTO
{
    public class NoteListDTO
    {
        public List<NoteDTO> NoteDTOs { get; set; }

        public NoteListDTO()
        {
            NoteDTOs = new List<NoteDTO>();
        }
    }
}
