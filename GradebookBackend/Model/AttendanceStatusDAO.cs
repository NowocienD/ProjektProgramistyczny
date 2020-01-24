using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class AttendanceStatusDAO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AttendanceDAO> Attendances { get; set; }
    }
}
