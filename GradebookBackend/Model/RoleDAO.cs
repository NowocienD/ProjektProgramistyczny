using System.Collections.Generic;

namespace GradebookBackend.Model
{
    public class RoleDAO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserDAO> Users { get; set; }
    }
}
