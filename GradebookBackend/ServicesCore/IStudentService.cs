﻿using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface IStudentService
    {
        public NoteListDTO GetStudentNotesByStuedntId(int studentId);
        public GradeListDTO GetStudentGradesByStudentId(int studentId, int subjectId);
        public int GetStudentClassIdByStudentId(int studentId);
        public StudentListDTO GetStudentsByClassId(int classId);
        public void AddStudent(int userId, int classId);
    }
}
