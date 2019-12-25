namespace GradebookBackend.ServicesCore
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(int userId);
    }
}