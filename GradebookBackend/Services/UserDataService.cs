using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System;
using System.Collections.Generic;

namespace GradebookBackend.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly IRepository<UserDAO> usersRepository;
        private readonly IRepository<RoleDAO> rolesRepository;
        private readonly IRepository<StudentDAO> studentsRepository;
        private readonly IRepository<TeacherDAO> teachersRepository;
        private readonly IRepository<AdminDAO> adminsRepository;

        public UserDataService(IRepository<UserDAO> usersRepository, IRepository<RoleDAO> rolesRepository, 
            IRepository<StudentDAO> studentsRepository, IRepository<AdminDAO> adminsRepository,
            IRepository<TeacherDAO> teachersRepository)
        {
            this.usersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
            this.studentsRepository = studentsRepository;
            this.teachersRepository = teachersRepository;
            this.adminsRepository = adminsRepository;
        }

        public UserDataDTO GetUserData(int Id)
        {
            UserDataDTO userDataDTO = new UserDataDTO();
            userDataDTO.Firstname = usersRepository.Get(Id).Firstname;
            userDataDTO.Surname = usersRepository.Get(Id).Surname;
            userDataDTO.Role = rolesRepository.Get(usersRepository.Get(Id).RoleId).Name;

            return userDataDTO;
        }
        public int GetUserIdByLoginAndPassword(string login, string password)
        {
            IEnumerable<UserDAO> users = usersRepository.GetAll();
            foreach(UserDAO user in users)
            {
                if(user.Login == login && user.Password == password)
                {
                    return user.Id;
                }
            }
            throw new GradebookServerException("User with this login and password don't exists");
        }
        public int GetStudentIdByUserId(int userId)
        {
            IEnumerable<StudentDAO> students = studentsRepository.GetAll();
            foreach (StudentDAO student in students)
            {
                if (student.UserId == userId)
                {
                    return student.UserId;
                }
            }
            throw new GradebookServerException("Student with this userId don't exist");
        }
        public int GetTeacherIdByUserId(int userId)
        {
            IEnumerable<TeacherDAO> teachers = teachersRepository.GetAll();
            foreach (TeacherDAO teacher in teachers)
            {
                if (teacher.UserId == userId)
                {
                    return teacher.UserId;
                }
            }
            throw new GradebookServerException("Teacher with this userId don't exist");
        }

        public bool IsAdmin(int userId)
        {
            IEnumerable<AdminDAO> listOfAdmins = adminsRepository.GetAll();
            foreach(AdminDAO admin in listOfAdmins)
            {
                if(admin.UserId == userId)
                {
                    return true;
                }
            }
            return false;
        }

    }
}