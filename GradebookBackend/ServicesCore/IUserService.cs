using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IUserService
    {
        void AddUser(UserDTO newUserDTO);
        void UpdateUser(UserDTO newUserDTO, int userId);
        void DeactivateUser(int userId);
        void UpdateUserPassword(UserPasswordChangeDTO userPasswordChangeDTO, int userId);
        bool CheckIfNewUserLoginIsUnique(string newUserLogin);
        bool CheckIfUpdatedUserLoginIsUnique(string updatedUserLogin, int userId);
        UserDataDTO GetUserDataByUserId(int userId);
        UserDTO GetUserByUserId(int userId);
        int GetUserIdByLoginAndPassword(string login, string password);
        int GetStudentIdByUserId(int userId);
        int GetTeacherIdByUserId(int userId);
        bool IsStudent(int userId);
        bool IsAdmin(int userId);
        bool IsTeacher(int userId);
        UserListDTO GetAllUsers();
    }
}