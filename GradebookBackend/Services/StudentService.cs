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
        private readonly IRepository<StudentDAO> studentRepository;

        public StudentService(IRepository<StudentDAO> studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public NoteListDTO GetStudentNotesByID(int studentId)
        {
            NoteListDTO noteListDTO = new NoteListDTO();
            List<NoteDAO> studentNoteList = studentRepository.Get(studentId).Notes;

            NoteDTO noteDTO;
            foreach(NoteDAO note in studentNoteList)
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
            IEnumerable<StudentDAO> students = studentRepository.GetAll();
            int studentId;
            foreach(StudentDAO student in students)
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
