using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public interface IClassSubjectService
    {
        public void AddClassSubject(int classId, int subjectId);
        public void DeleteClassSubject(int classId, int subjectId);
    }
}
