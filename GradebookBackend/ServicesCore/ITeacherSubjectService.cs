using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface ITeacherSubjectService
    {
        public TeacherSubjectListDTO GetTeacherSubjectBySubjectId(int subjectId);
        public void AddTeacherSubject(int teacherId, int subjectId);
        public void DeleteTeacherSubject(int teacherId, int subjectId);
    }
}
