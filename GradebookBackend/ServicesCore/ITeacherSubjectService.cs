using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface ITeacherSubjectService
    {
        public TeacherSubjectListDTO GetTeacherSubjectBySubjectId(int subjectId);
        public void AddTeacherSubject(int teacherId, int subjectId);
        public void DeleteTeacherSubject(int teacherId, int subjectId);
    }
}
