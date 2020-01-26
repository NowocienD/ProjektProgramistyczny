using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace GradebookBackend.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepository<NoteDAO> notesRepository;
        private readonly IRepository<UserDAO> usersRepository;
        private readonly IRepository<TeacherDAO> teachersRepository;

        public NoteService(IRepository<TeacherDAO> teachersRepository, IRepository<UserDAO> usersRepository,
            IRepository<NoteDAO> notesRepository)
        {
            this.notesRepository = notesRepository;
            this.usersRepository = usersRepository;
            this.teachersRepository = teachersRepository;
        }

        public NoteListDTO GetStudentNotesByStudentId(int studentId)
        {
            NoteListDTO studentNotesDTO = new NoteListDTO();
            IEnumerable<NoteDAO> allNotesDAO = notesRepository.GetAll();
            foreach (NoteDAO note in allNotesDAO)
            {
                if (note.StudentId == studentId)
                {
                    string teacherFirstName = usersRepository.Get(teachersRepository.Get(note.TeacherId).UserId).Firstname;
                    string teacherSurname = usersRepository.Get(teachersRepository.Get(note.TeacherId).UserId).Surname;
                    NoteDTO noteDTO = new NoteDTO
                    {
                        Statement = note.Statement,
                        Date = note.Date.ToString("yyyy-MM-dd"),
                        TeacherFirstName = teacherFirstName,
                        TeacherSurname = teacherSurname,
                        FirstNameAndSurname = teacherFirstName + " " + teacherSurname

                    };
                    studentNotesDTO.NoteDTOs.Add(noteDTO);
                }
            }
            return studentNotesDTO;
        }

        public void AddNewNote(NoteDTO newNoteDTO, int teacherId, int studentId)
        {
            NoteDAO newNoteDAO = new NoteDAO
            {
                Statement = newNoteDTO.Statement,
                Date = DateTime.ParseExact(newNoteDTO.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                StudentId = studentId,
                TeacherId = teacherId
            };
            notesRepository.Add(newNoteDAO);
        }
    }
}
