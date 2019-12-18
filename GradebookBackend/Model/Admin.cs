using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class Admin
    {
        public int Id { get; set; }
        public User User { get; set; }
    }
}
