using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IUserService
    {
        public void AddUser(UserDTO newUserDTO);
        public void UpdateUser(UserDTO newUserDTO, int userId);
        public void DeactivateUser(int userId);
        public void UpdateUserPassword(UserPasswordChangeDTO userPasswordChangeDTO, int userId);
        public bool CheckIfNewUserLoginIsUnique(string newUserLogin);
        public bool CheckIfUpdatedUserLoginIsUnique(string updatedUserLogin, int userId);
        UserDataDTO GetUserDataByUserId(int userId);
        public UserDTO GetUserByUserId(int userId);
        public int GetUserIdByLoginAndPassword(string login, string password);
        public int GetStudentIdByUserId(int userId);
        public int GetTeacherIdByUserId(int userId);
        public bool IsStudent(int userId);
        public bool IsAdmin(int userId);
        public bool IsTeacher(int userId);
        public UserListDTO GetAllUsers();
    }
}