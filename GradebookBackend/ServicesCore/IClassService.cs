using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface IClassService
    {
        public ClassListDTO GetAllClasses();
        public ClassListDTO GetAllClassesOfTeacher(int teacherId);
        public void AddClass(ClassDTO newClassDTO);
        public void DeleteClassWithId(int classId);
    }
}
