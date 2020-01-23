using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class TeacherSubjectRepository : IRepositoryRelation<TeacherSubjectDAO>
    {
        private readonly GradebookDbContext context;
        public TeacherSubjectRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public TeacherSubjectDAO Add(TeacherSubjectDAO tObject)
        {
            this.context.TeachersSubjects.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public TeacherSubjectDAO Delete(int teacherId, int subjectId)
        {
            TeacherSubjectDAO tObject = this.context.TeachersSubjects.Find(teacherId, subjectId);
            if (tObject != null)
            {
                this.context.TeachersSubjects.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public TeacherSubjectDAO Get(int teacherId, int subjectId)
        {
            return this.context.TeachersSubjects.Find(teacherId, subjectId);
        }

        public IEnumerable<TeacherSubjectDAO> GetAll()
        {
            return this.context.TeachersSubjects;
        }

        public TeacherSubjectDAO Update(TeacherSubjectDAO tObjectChanges)
        {
            var tObject = this.context.TeachersSubjects.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
