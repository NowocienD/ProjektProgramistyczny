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
    public class TeacherService : ITeacherService
    {
        public IRepository<TeacherDAO> teacherRepository;
        public IRepository<UserDAO> userRepository;
        public TeacherService(IRepository<TeacherDAO> teacherRepository, IRepository<UserDAO> userRepository)
        {
            this.teacherRepository = teacherRepository;
            this.userRepository = userRepository;
        }

        public TeacherListDTO GetAllTeachers()
        {
            IEnumerable<TeacherDAO> teachers = teacherRepository.GetAll();
            TeacherListDTO teacherListDTO = new TeacherListDTO();
            foreach(TeacherDAO teacher in teachers)
            {
                teacherListDTO.teachers.Add(new TeacherDTO
                {
                    Id = teacher.Id,
                    FirstnameSurname = userRepository.Get(teacher.UserId).Firstname + " " + userRepository.Get(teacher.UserId).Surname
                });
            }
            return teacherListDTO;
        }
    }
}
