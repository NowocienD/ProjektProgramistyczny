using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface INoteService
    {
        public NoteListDTO GetStudentNotesByStudentId(int studentId);
        public void AddNewNote(NoteDTO newNoteDTO, int teacherId, int studentId);
    }
}
