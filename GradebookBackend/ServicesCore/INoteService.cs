using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface INoteService
    {
        NoteListDTO GetStudentNotesByStudentId(int studentId);
        void AddNewNote(NoteDTO newNoteDTO, int teacherId, int studentId);
    }
}
