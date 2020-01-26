
namespace GradebookBackend.DTO
{
    public class SingleLessonAttendanceDTO
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int AttendanceStatusId { get; set; }
        public string AttendanceStatus { get; set; }
    }
}
