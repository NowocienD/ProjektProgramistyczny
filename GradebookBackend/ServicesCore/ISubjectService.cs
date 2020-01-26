using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface ISubjectService
    {
        SubjectListDTO GetSubjectListByClassId(int classId);
        SubjectListDTO GetAllSubjects();
        void AddNewSubject(SubjectDTO newSubjectDTO);
        void DeleteSubject(int subjectId);
        void UpdateSubject(SubjectDTO updatedSubjectDTO, int subjectId);
    }
}
