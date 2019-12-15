using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class Class
    {
     //   public Class()
     //   {
       //     Subjects = new List<Subject>();
       // }
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}
