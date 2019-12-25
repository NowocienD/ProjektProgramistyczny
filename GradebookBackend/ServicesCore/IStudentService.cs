using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface IStudentService
    {
        public NoteListDTO GetStudentGradesByID(int studentId);
        public NoteListDTO GetStudentNotesByUserId(int userId);
        public GradeListDTO GetStudentGradesByStudentId(int studentId, int subjectId);
    }
}
