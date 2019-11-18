namespace GradeBook.API
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(int userId);
    }
}