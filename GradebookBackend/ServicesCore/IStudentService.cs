using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface IStudentService
    {
        public NoteListDTO GetStudentNotesByID(int id);
    }
}
