using System.Collections.Generic;

namespace GradebookBackend.Model
{
    public class AttendanceStatusDAO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AttendanceDAO> Attendances { get; set; }
    }
}
