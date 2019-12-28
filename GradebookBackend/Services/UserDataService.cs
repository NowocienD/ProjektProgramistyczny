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
        private readonly IRepository<AdminDAO> adminRepository;

        public UserDataService(IRepository<UserDAO> usersRepository, IRepository<RoleDAO> rolesRepository, 
            IRepository<StudentDAO> studentsRepository, IRepository<AdminDAO> adminRepository)
        {
            this.usersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
            this.studentsRepository = studentsRepository;
            this.adminRepository = adminRepository;
        }

        public UserDataDTO GetUserData(int Id)
        {
            UserDataDTO userDataDTO = new UserDataDTO();
            userDataDTO.Firstname = usersRepository.Get(Id).Firstname;
            userDataDTO.Surname = usersRepository.Get(Id).Surname;
            userDataDTO.Role = rolesRepository.Get(usersRepository.Get(Id).RoleId).Name;

            return userDataDTO;
        }
        // uzywane tylko do tworzenia tokenu
        public int GetUserId(string login, string password)
        {
            int userId = 0;
            IEnumerable<UserDAO> users = usersRepository.GetAll();
            foreach(UserDAO user in users)
            {
                if(user.Login == login && user.Password == password)
                {
                    userId = user.Id;
                }
            }
            return userId;
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
            return -1;
        }

        public bool IsAdmin(int userId)
        {
            IEnumerable<AdminDAO> listOfAdmins = adminRepository.GetAll();
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