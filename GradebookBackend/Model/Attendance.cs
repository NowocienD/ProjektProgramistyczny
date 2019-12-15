using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class Attendance
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string State { get; set; }

        public Lesson Lesson { get; set; }
    }
}
