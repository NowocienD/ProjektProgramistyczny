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
    public class TeacherSubjectService : ITeacherSubjectService
    {
        public IRepository<TeacherSubjectDAO> teacherSubjectRepository;
        public IRepository<TeacherDAO> teacherRepository;
        public IRepository<UserDAO> userRepository;
        public TeacherSubjectService(IRepository<TeacherSubjectDAO> teacherSubjectRepository,
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
            foreach(TeacherSubjectDAO teacherSubject in teacherSubjects)
            {
                if(teacherSubject.SubjectId == subjectId)
                {
                    int userId = teacherRepository.Get(teacherSubject.TeacherId).UserId;
                    teacherSubjectListDTO.teacherSubjects.Add(new TeacherSubjectDTO
                    {
                        TeacherIdFirstnameSurname = teacherSubject.TeacherId + " " + userRepository.Get(userId).Firstname + " " + userRepository.Get(userId).Surname
                    });
                }
            }
            return teacherSubjectListDTO;
        }
    }
}
