
namespace GradebookBackend.Model
{
    public class ClassSubjectDAO
    {
        public int ClassId { get; set; }
        public virtual ClassDAO Class { get; set; }

        public int SubjectId { get; set; }
        public virtual SubjectDAO Subject { get; set; }
    }
}
