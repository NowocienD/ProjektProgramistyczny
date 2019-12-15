using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class Subject
    {
       // public Subject()
       // {
       //     Classes = new List<Class>();
        //}
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Lesson> Lessons { get; set; }
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        public List<Grade> Grades { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
    }
}
