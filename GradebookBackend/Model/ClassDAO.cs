using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class ClassDAO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<ClassSubjectDAO> ClassSubjects { get; set; }
        public List<LessonDAO> Lessons { get; set; }
    }
}
