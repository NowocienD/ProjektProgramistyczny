using System.Collections.Generic;

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
