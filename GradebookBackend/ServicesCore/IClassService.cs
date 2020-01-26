using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IClassService
    {
        public ClassListDTO GetAllClasses();
        public ClassListDTO GetAllClassesOfTeacher(int teacherId);
        public void AddClass(ClassDTO newClassDTO);
        public void DeleteClass(int classId);
        public void UpdateClass(ClassDTO updatedClassDTO, int classId);
    }
}
