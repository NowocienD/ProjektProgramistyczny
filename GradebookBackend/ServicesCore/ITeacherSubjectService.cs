using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface ITeacherSubjectService
    {
        TeacherSubjectListDTO GetTeacherSubjectBySubjectId(int subjectId);
        void AddTeacherSubject(int teacherId, int subjectId);
        void DeleteTeacherSubject(int teacherId, int subjectId);
    }
}
