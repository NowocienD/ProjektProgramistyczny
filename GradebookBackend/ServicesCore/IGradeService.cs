using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface IGradeService
    {
        public bool CheckIfTeacherTeachSubject(int teacherId, int subjectId);
        public void AddGrade(NewGradeDTO newGradeDTO, int teacherId, int studentId);
        public void DeleteGrade(int gradeId);
        public void UpdateGrade(NewGradeDTO newGradeDTO, int teacherId, int studentId);
        public GradeListDTO GetStudentGradesByStudentId(int studentId, int subjectId);
    }
}
