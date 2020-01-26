using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface ITeacherService
    {
        public TeacherListDTO GetAllTeachers();
    }
}
