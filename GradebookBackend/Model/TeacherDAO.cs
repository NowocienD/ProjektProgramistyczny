using System.Collections.Generic;

namespace GradebookBackend.Model
{
    public class TeacherDAO
    {
        public int Id { get; set; }
        public List<NoteDAO> Notes { get; set; }
        public List<GradeDAO> Grades { get; set; }
        public virtual ICollection<TeacherSubjectDAO> TeacherSubjects { get; set; }
        public List<LessonDAO> Lessons { get; set; }

        public int UserId { get; set; }
        public UserDAO User { get; set; }
    }
}
