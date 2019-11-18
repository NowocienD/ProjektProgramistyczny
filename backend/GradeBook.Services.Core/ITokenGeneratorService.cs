namespace GradeBook.Services.Core
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(int userId);
    }
}