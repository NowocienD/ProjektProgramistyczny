using System.Collections.Generic;

namespace GradebookBackend.Model
{
    public class StudentDAO
    {
        public int Id { get; set; }

        public List<AttendanceDAO> Attendances { get; set; }
        public List<GradeDAO> Grades { get; set; }
        public List<NoteDAO> Notes { get; set; }

        public int ClassId { get; set; }
        public ClassDAO Class { get; set; }

        public int UserId { get; set; }
        public UserDAO User { get; set; }
    }
}
