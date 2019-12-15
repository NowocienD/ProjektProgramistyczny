using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class Teacher
    {
        public int Id { get; set; }
        public User User { get; set; }

        public List<Note> Notes { get; set; }
        public List<Grade> Grades { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
