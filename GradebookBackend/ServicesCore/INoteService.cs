using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface INoteService
    {
        public NoteListDTO GetStudentNotesByStudentId(int studentId);
        public void AddNewNote(NoteDTO newNoteDTO, int teacherId, int studentId);
    }
}
