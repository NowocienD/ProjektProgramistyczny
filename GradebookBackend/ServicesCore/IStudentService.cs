﻿using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface IStudentService
    {
        public int GetStudentClassIdByStudentId(int studentId);
        public StudentListDTO GetStudentsByClassId(int classId);
    }
}
