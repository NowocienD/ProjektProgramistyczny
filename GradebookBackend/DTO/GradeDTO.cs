using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.DTO
{
    public class GradeDTO
    {
        public string Date { get; set; }
        public int Importance { get; set; }
        public int Value { get; set; }
        public string Topic { get; set; }
        public string TeacherFirstname { get; set; }
        public string TeacherSurname { get; set; }
        public string TeacherFullname { get; set; }
    }
}
