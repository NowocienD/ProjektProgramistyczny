using GradebookBackend.DTO;

namespace GradebookBackend
{
    public interface IUserDataService
    {
        UserDataDTO GetUserData(int id);
    }
}