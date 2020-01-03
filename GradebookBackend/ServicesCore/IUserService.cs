using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IUserService
    {
        public void AddUser(NewUserDTO newUserDTO);
        public void UpdateUser(NewUserDTO newUserDTO, int userId);
        public bool CheckIfNewUserLoginIsUnique(string newUserLogin);
        public bool CheckIfUpdatedUserLoginIsUnique(string updatedUserLogin, int userId);
        UserDataDTO GetUserDataByUserId(int userId);
        public int GetUserIdByLoginAndPassword(string login, string password);
        public int GetStudentIdByUserId(int userId);
        public int GetTeacherIdByUserId(int userId);
        public bool IsAdmin(int userId);
        public bool IsTeacher(int userId);
    }
}