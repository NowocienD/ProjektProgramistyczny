using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class Note
    {
        public int Id { get; set; }
        string statement { get; set; }

        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
    }
}
