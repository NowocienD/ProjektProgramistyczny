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
        private readonly IRepository<StudentDAO> studentsRepository;
        private readonly IRepository<GradeDAO> gradesRepository;
        private readonly IRepository<TeacherDAO> teachersRepository;
        private readonly IRepository<UserDAO> usersRepository;

        public StudentService(IRepository<StudentDAO> studentsRepository, IRepository<GradeDAO> gradesRepository,
            IRepository<TeacherDAO> teachersRepository, IRepository<UserDAO> usersRepository)
        {
            this.studentsRepository = studentsRepository;
            this.gradesRepository = gradesRepository;
            this.teachersRepository = teachersRepository;
            this.usersRepository = usersRepository;
        }

        public NoteListDTO GetStudentNotesByStuedntId(int studentId)
        {
            NoteListDTO noteListDTO = new NoteListDTO();
            List<NoteDAO> studentNoteList = studentsRepository.Get(studentId).Notes;

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
            IEnumerable<StudentDAO> students = studentsRepository.GetAll();
            int studentId;
            foreach(StudentDAO student in students)
            {
                if(student.UserId == userId)
                {
                    studentId = student.Id;
                    return GetStudentNotesByStuedntId(studentId);
                } 
            }
            throw new KeyNotFoundException("There is no student with this userId");
        }

        public GradeListDTO GetStudentGradesByStudentId(int studentId, int subjectId)
        {
            IEnumerable<GradeDAO> grades = gradesRepository.GetAll();
            GradeListDTO gradeListDTO = new GradeListDTO();
            int studentelo = studentId;
            foreach (GradeDAO grade in grades)
            {
                if (grade.StudentId == studentId && grade.SubjectId == subjectId)
                {
                    gradeListDTO.GradeDTOs.Add(new GradeDTO
                    {
                        Value = grade.Value,
                        Importance = grade.Importance,
                        Topic = grade.Topic,
                        Date = grade.Date,
                        TeacherFirstname = usersRepository.Get(teachersRepository.Get(grade.TeacherId).UserId).Firstname,
                        TeacherSurname = usersRepository.Get(teachersRepository.Get(grade.TeacherId).UserId).Surname
                }); 
                }
            }
            return gradeListDTO; 
        }
    }
}
