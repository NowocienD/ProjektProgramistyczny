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
        private readonly IRepository<NoteDAO> notesRepository;

        public StudentService(IRepository<StudentDAO> studentsRepository, IRepository<GradeDAO> gradesRepository,
            IRepository<TeacherDAO> teachersRepository, IRepository<UserDAO> usersRepository, IRepository<NoteDAO> notesRepository)
        {
            this.studentsRepository = studentsRepository;
            this.gradesRepository = gradesRepository;
            this.teachersRepository = teachersRepository;
            this.usersRepository = usersRepository;
            this.notesRepository = notesRepository;
        }

        //przeniesc do serwisu NotesService
        public NoteListDTO GetStudentNotesByStuedntId(int studentId)
        {
            NoteListDTO studentNotesDTO = new NoteListDTO();
            IEnumerable<NoteDAO> allNotesDAO = notesRepository.GetAll();
            foreach (NoteDAO note in allNotesDAO)
            {
                if (note.StudentId == studentId)
                {
                    NoteDTO noteDTO = new NoteDTO
                    {
                        Statement = note.Statement,
                        TeacherFirstName = usersRepository.Get(teachersRepository.Get(note.TeacherId).UserId).Firstname,
                        TeacherSurname = usersRepository.Get(teachersRepository.Get(note.TeacherId).UserId).Surname
                    };
                    studentNotesDTO.NoteDTOs.Add(noteDTO);
                }
            }
            return studentNotesDTO;

        }

        //przeniesc do serwisu GradeService
        public GradeListDTO GetStudentGradesByStudentId(int studentId, int subjectId)
        {
            IEnumerable<GradeDAO> grades = gradesRepository.GetAll();
            GradeListDTO gradeListDTO = new GradeListDTO();
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

        public StudentListDTO GetStudentsByClassId(int classId)
        {
            IEnumerable<StudentDAO> students = studentsRepository.GetAll();
            StudentListDTO studentListDTO = new StudentListDTO();
            foreach(StudentDAO student in students)
            {
                if(student.ClassId == classId)
                {
                    StudentDTO studentToAdd = new StudentDTO()
                    {
                        Id = student.Id,
                        Name = usersRepository.Get(student.UserId).Firstname,
                        Surname = usersRepository.Get(student.UserId).Surname
                    };
                    studentListDTO.studentList.Add(studentToAdd);
                }
            }
            return studentListDTO;
        }

        public int GetStudentClassIdByStudentId(int studentId)
        {
            return studentsRepository.Get(studentId).ClassId;
        }
    }
}
