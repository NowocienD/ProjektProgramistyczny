namespace GradebookBackend
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(int userId);
    }
}