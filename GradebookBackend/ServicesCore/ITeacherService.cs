using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface ITeacherService
    {
        TeacherListDTO GetAllTeachers();
    }
}
