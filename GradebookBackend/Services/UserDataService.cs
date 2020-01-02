using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
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

        private readonly PasswordHasher passwordHasher;

        public UserDataService(IRepository<UserDAO> usersRepository, IRepository<RoleDAO> rolesRepository, 
            IRepository<StudentDAO> studentsRepository, IRepository<AdminDAO> adminsRepository,
            IRepository<TeacherDAO> teachersRepository)
        {
            this.usersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
            this.studentsRepository = studentsRepository;
            this.teachersRepository = teachersRepository;
            this.adminsRepository = adminsRepository;

            this.passwordHasher = new PasswordHasher();
        }
        public void AddUser(NewUserDTO newUserDTO)
        {
            UserDAO newUserDAO = new UserDAO
            {
                Login = newUserDTO.Login,
                Email = newUserDTO.Email,
                Firstname = newUserDTO.Firstname,
                Surname = newUserDTO.Surname,
                RoleId = newUserDTO.RoleId
            };
            newUserDAO.Password = passwordHasher.HashPassword(newUserDTO.Password);
            usersRepository.Add(newUserDAO);
        }

        public UserDataDTO GetUserDataByUserId(int userId)
        {
            UserDataDTO userDataDTO = new UserDataDTO();
            userDataDTO.Firstname = usersRepository.Get(userId).Firstname;
            userDataDTO.Surname = usersRepository.Get(userId).Surname;
            userDataDTO.Role = rolesRepository.Get(usersRepository.Get(userId).RoleId).Name;

            return userDataDTO;
        }
        public int GetUserIdByLoginAndPassword(string login, string password)
        {
            IEnumerable<UserDAO> users = usersRepository.GetAll();
            foreach(UserDAO user in users)
            {
                if(user.Login == login && passwordHasher.VerifyHashedPassword(user.Password, password) == Microsoft.AspNet.Identity.PasswordVerificationResult.Success)
                {
                    return user.Id;
                }
            }
            throw new GradebookServerException("User with this login and password doesn't exists");
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
            throw new GradebookServerException("Student with this userId doesn't exist");
        }
        public int GetTeacherIdByUserId(int userId)
        {
            IEnumerable<TeacherDAO> teachers = teachersRepository.GetAll();
            foreach (TeacherDAO teacher in teachers)
            {
                if (teacher.UserId == userId)
                {
                    return teacher.Id;
                }
            }
            throw new GradebookServerException("Teacher with this userId doesn't exist");
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
        public bool IsTeacher(int userId)
        {
            IEnumerable<TeacherDAO> teachers = teachersRepository.GetAll();
            foreach (TeacherDAO teacher in teachers)
            {
                if (teacher.UserId == userId)
                {
                    return true;
                }
            }
            return false;
        }

    }
}