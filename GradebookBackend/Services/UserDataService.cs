using GradebookBackend.DTO;
using GradebookBackend.Model;
using GradebookBackend.Repositories;
using System;

namespace GradebookBackend
{
    public class UserDataService : IUserDataService
    {
        private readonly IRepository<User> userRepository;

        public UserDataService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }
        public UserDataDTO GetUserData(int id)
        {
            UserDataDTO userDataDTO = new UserDataDTO();
            userDataDTO.Firstname = userRepository.Get(id).Firstname;
            userDataDTO.Surname = userRepository.Get(id).Surname;
            userDataDTO.Role = userRepository.Get(id).Role.name;
            return userDataDTO;
        }
    }
}