using GradebookBackend.Controllers;
using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> studentRepository;

        public StudentService(IRepository<Student> studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public NoteListDTO GetStudentNotesByID(int studentId)
        {
            NoteListDTO noteListDTO = new NoteListDTO();
            List<Note> studentNoteList = studentRepository.Get(studentId).Notes;

            NoteDTO noteDTO;
            foreach(Note note in studentNoteList)
            {
                noteDTO = new NoteDTO
                {
                    Statement = note.Statement,
                    TeacherFirstName = note.Teacher.User.Firstname,
                    TeacherSurname = note.Teacher.User.Surname
                };
                noteListDTO.NoteDTOs.Add(noteDTO);
            }
            return noteListDTO;
        }

        public NoteListDTO GetStudentNotesByUserId(int userId)
        {
            IEnumerable<Student> students = studentRepository.GetAll();
            int studentId;
            foreach(Student student in students)
            {
                if(student.UserId == userId)
                {
                    studentId = student.Id;
                    return GetStudentNotesByID(studentId);
                } 
            }
            throw new KeyNotFoundException("there is no student with this userId");
        }
    }
}
