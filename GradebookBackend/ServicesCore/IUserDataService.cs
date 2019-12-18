namespace GradebookBackend
{
    public interface IUserDataService
    {
        int GetId(string userName);
        
        string GetUsername(int id);
        
        string GetAccountType(int id);
    }
}