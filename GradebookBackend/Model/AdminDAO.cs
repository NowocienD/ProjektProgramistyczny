namespace GradebookBackend.Model
{
    public class AdminDAO
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserDAO User { get; set; }
    }
}
