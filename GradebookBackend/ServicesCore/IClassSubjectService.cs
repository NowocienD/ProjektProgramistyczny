using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IClassSubjectService
    {
        void AddClassSubject(int classId, int subjectId);
        void DeleteClassSubject(int classId, int subjectId);
        SubjectListDTO GetSubjectsAssignedToClass(int classId);
    }
}
