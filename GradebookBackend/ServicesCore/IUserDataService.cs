using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IUserDataService
    {
        public void AddUser(NewUserDTO newUserDTO);
        public void DeleteUser(int userId);
        UserDataDTO GetUserDataByUserId(int userId);
        public int GetUserIdByLoginAndPassword(string login, string password);
        public int GetStudentIdByUserId(int userId);
        public int GetTeacherIdByUserId(int userId);
        public bool IsAdmin(int userId);
        public bool IsTeacher(int userId);
    }
}