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
        private readonly IRepository<UserDAO> usersRepository;

        public StudentService(IRepository<StudentDAO> studentsRepository, IRepository<UserDAO> usersRepository)
        {
            this.studentsRepository = studentsRepository;
            this.usersRepository = usersRepository;
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
                        Firstname = usersRepository.Get(student.UserId).Firstname,
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
