using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IClassSubjectService
    {
        public void AddClassSubject(int classId, int subjectId);
        public void DeleteClassSubject(int classId, int subjectId);
        public SubjectListDTO GetSubjectsAssignedToClass(int classId);
    }
}
