
namespace GradebookBackend.Model
{
    public class UserDAO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }

        public int RoleId { get; set; }
        public RoleDAO Role { get; set; }
    }
}
