using GradebookBackend.DTO;

namespace GradebookBackend
{
    public interface IUserDataService
    {
        UserDataDTO GetUserData(int id);
        public int GetUserId(string login, string password);
        public int GetStudentIdByUserId(int userId);
    }
}