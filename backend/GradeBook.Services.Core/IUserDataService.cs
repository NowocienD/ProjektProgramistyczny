namespace GradeBook.Services.Core
{
    public interface IUserData
    {
        int GetId(string userName);
        
        string GetUsername(int Id);
        
        string GetAccountType(int Id);
    }
}