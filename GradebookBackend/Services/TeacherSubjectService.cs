using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System.Collections.Generic;

namespace GradebookBackend.Services
{
    public class TeacherSubjectService : ITeacherSubjectService
    {
        public IRepositoryRelation<TeacherSubjectDAO> teacherSubjectRepository;
        public IRepository<TeacherDAO> teacherRepository;
        public IRepository<UserDAO> userRepository;
        public TeacherSubjectService(IRepositoryRelation<TeacherSubjectDAO> teacherSubjectRepository,
            IRepository<TeacherDAO> teacherRepository, IRepository<UserDAO> userRepository)
        {
            this.teacherSubjectRepository = teacherSubjectRepository;
            this.teacherRepository = teacherRepository;
            this.userRepository = userRepository;
        }
        public TeacherSubjectListDTO GetTeacherSubjectBySubjectId(int subjectId)
        {
            IEnumerable<TeacherSubjectDAO> teacherSubjects = teacherSubjectRepository.GetAll();
            TeacherSubjectListDTO teacherSubjectListDTO = new TeacherSubjectListDTO();
            foreach (TeacherSubjectDAO teacherSubject in teacherSubjects)
            {
                if (teacherSubject.SubjectId == subjectId)
                {
                    int userId = teacherRepository.Get(teacherSubject.TeacherId).UserId;
                    teacherSubjectListDTO.teacherSubjects.Add(new TeacherSubjectDTO
                    {
                        Id = teacherSubject.TeacherId,
                        FirstnameSurname = userRepository.Get(userId).Firstname + " " + userRepository.Get(userId).Surname
                    });
                }
            }
            return teacherSubjectListDTO;
        }

        public void AddTeacherSubject(int teacherId, int subjectId)
        {
            TeacherSubjectDAO newTeacherSubject = new TeacherSubjectDAO
            {
                TeacherId = teacherId,
                SubjectId = subjectId
            };
            teacherSubjectRepository.Add(newTeacherSubject);
        }

        public void DeleteTeacherSubject(int teacherId, int subjectId)
        {
            teacherSubjectRepository.Delete(teacherId, subjectId);
        }
    }
}
