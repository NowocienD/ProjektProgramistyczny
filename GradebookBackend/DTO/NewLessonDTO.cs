
namespace GradebookBackend.DTO
{
    public class NewLessonDTO
    {
        public int LessonNumber { get; set; }
        public int DayOfTheWeek { get; set; }
        public int SubjectId { get; set; }
        public int ClassId { get; set; }
        public int TeacherId { get; set; }
    }
}
