using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Services
{
    public class StudentService
    {
        private readonly IRepository<Student> studentRepository;

        public StudentService(IRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public NoteListDTO GetStudentNotesByID(int id)
        {
            NoteListDTO noteListDTO = new NoteListDTO();
            List<Note> studentNoteList = studentRepository.Get(id).Notes;

            NoteDTO noteDTO;
            foreach(Note note in studentNoteList)
            {
                noteDTO = new NoteDTO
                {
                    statement = note.Statement,
                    teacherFirstName = note.Teacher.User.Firstname,
                    teacherSurname = note.Teacher.User.Surname
                };
                noteListDTO.NoteDTOs.Add(noteDTO);
            }
            return noteListDTO;
        }
    }
}
