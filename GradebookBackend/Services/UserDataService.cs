using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using GradebookBackend.ServicesCore;
using System;
using System.Collections.Generic;

namespace GradebookBackend
{
    public class UserDataService : IUserDataService
    {
        private readonly IRepository<UserDAO> usersRepository;
        private readonly IRepository<RoleDAO> rolesRepository;

        public UserDataService(IRepository<UserDAO> usersRepository, IRepository<RoleDAO> rolesRepository)
        {
            this.usersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
        }
        public UserDataDTO GetUserData(int Id)
        {
            UserDataDTO userDataDTO = new UserDataDTO();
            userDataDTO.Firstname = usersRepository.Get(Id).Firstname;
            userDataDTO.Surname = usersRepository.Get(Id).Surname;
            userDataDTO.Role = rolesRepository.Get(usersRepository.Get(Id).RoleId).Name;

            return userDataDTO;
        }
        public int GetUserId(string login, string password)
        {
            int Id = 0;
            IEnumerable<UserDAO> users = usersRepository.GetAll();
            foreach(UserDAO user in users)
            {
                if(user.Login == login && user.Password == password)
                {
                    Id = user.Id;
                }
            }
            return Id;
        }
    }
}