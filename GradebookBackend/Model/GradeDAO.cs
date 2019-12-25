using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class GradeDAO
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Importance { get; set; }
        public int Value { get; set; }
        public string Topic { get; set; }

        public int SubjectId { get; set; }
        public SubjectDAO Subject { get; set; }

        public int StudentId { get; set; }
        public StudentDAO Student { get; set; }

        public int TeacherId { get; set; }
        public TeacherDAO Teacher { get; set; }
    }
}
