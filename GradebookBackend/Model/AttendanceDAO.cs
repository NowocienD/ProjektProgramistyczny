﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradebookBackend.Model
{
    public class AttendanceDAO
    {
        public int Id { get; set; }
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public int AttendanceStatusId { get; set; }
        public AttendanceStatusDAO AttendanceStatus { get; set; }

        public int LessonId { get; set; }
        public LessonDAO Lesson { get; set; }

        public int StudentId { get; set; }
        public StudentDAO Student { get; set; }
    }
}
