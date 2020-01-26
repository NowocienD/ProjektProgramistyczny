using System.Collections.Generic;

namespace GradebookBackend.Model
{
    public class SubjectDAO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<LessonDAO> Lessons { get; set; }
        public virtual ICollection<ClassSubjectDAO> ClassSubjects { get; set; }
        public List<GradeDAO> Grades { get; set; }
        public virtual ICollection<TeacherSubjectDAO> TeacherSubjects { get; set; }
    }
}
