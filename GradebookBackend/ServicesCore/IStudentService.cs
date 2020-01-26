using GradebookBackend.DTO;

namespace GradebookBackend.ServicesCore
{
    public interface IStudentService
    {
        public int GetStudentClassIdByStudentId(int studentId);
        public StudentListDTO GetStudentsByClassId(int classId);
    }
}
