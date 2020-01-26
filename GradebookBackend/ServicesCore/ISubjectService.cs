using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface ISubjectService
    {
        public SubjectListDTO GetSubjectListByClassId(int classId);
        public SubjectListDTO GetAllSubjects();
        public void AddNewSubject(SubjectDTO newSubjectDTO);
        public void DeleteSubject(int subjectId);
        public void UpdateSubject(SubjectDTO updatedSubjectDTO, int subjectId);
    }
}
