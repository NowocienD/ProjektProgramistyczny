
namespace GradebookBackend.Model
{
    public class TeacherSubjectDAO
    {
        public int TeacherId { get; set; }
        public virtual TeacherDAO Teacher { get; set; }

        public int SubjectId { get; set; }
        public virtual SubjectDAO Subject { get; set; }
    }
}
