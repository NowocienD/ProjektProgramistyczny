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
    }
}
