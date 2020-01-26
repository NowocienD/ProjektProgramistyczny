using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IStudentService
    {
        int GetStudentClassIdByStudentId(int studentId);
        StudentListDTO GetStudentsByClassId(int classId);
    }
}
