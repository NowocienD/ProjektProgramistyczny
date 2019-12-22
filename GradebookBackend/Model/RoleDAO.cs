using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class RoleDAO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserDAO> Users { get; set; }
    }
}
