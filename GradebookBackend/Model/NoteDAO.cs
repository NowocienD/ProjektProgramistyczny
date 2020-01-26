using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradebookBackend.Model
{
    public class NoteDAO
    {
        public int Id { get; set; }
        public string Statement { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public int StudentId { get; set; }
        public StudentDAO Student { get; set; }

        public int TeacherId { get; set; }
        public TeacherDAO Teacher { get; set; }
    }
}
