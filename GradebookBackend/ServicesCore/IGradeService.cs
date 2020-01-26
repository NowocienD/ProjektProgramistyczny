using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IGradeService
    {
        bool CheckIfTeacherTeachSubject(int teacherId, int subjectId);
        void AddGrade(NewGradeDTO newGradeDTO, int teacherId, int studentId);
        void DeleteGrade(int gradeId);
        void UpdateGrade(NewGradeDTO newGradeDTO, int teacherId, int studentId);
        GradeListDTO GetStudentGradesByStudentId(int studentId, int subjectId);
    }
}
