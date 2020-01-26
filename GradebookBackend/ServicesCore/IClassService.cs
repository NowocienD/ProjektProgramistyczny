using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IClassService
    {
        ClassListDTO GetAllClasses();
        ClassListDTO GetAllClassesOfTeacher(int teacherId);
        void AddClass(ClassDTO newClassDTO);
        void DeleteClass(int classId);
        void UpdateClass(ClassDTO updatedClassDTO, int classId);
    }
}
