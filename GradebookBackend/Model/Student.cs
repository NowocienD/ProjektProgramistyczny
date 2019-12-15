using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class Student
    {
        public int Id { get; set; }
        public User User { get; set; }

        public List<Attendance> Attendances { get; set; }
        public List<Grade> Grades { get; set; }
        public List<Note> Notes { get; set; }
        public Class Class { get; set; }
    }
}
