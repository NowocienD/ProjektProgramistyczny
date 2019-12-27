using GradebookBackend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface ILessonService
    {
        public LessonPlanDTO GetLessonPlanByClassId(int classId);
    }
}
