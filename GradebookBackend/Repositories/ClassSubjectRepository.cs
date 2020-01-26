using GradebookBackend.Model;
using System.Collections.Generic;

namespace GradebookBackend.Repositories
{
    public class ClassSubjectRepository : IRepositoryRelation<ClassSubjectDAO>
    {
        private readonly GradebookDbContext context;

        public ClassSubjectRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public ClassSubjectDAO Add(ClassSubjectDAO tObject)
        {
            this.context.ClassesSubjects.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public ClassSubjectDAO Delete(int classId, int subjectId)
        {
            ClassSubjectDAO tObject = this.context.ClassesSubjects.Find(classId, subjectId);
            if (tObject != null)
            {
                this.context.ClassesSubjects.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public ClassSubjectDAO Get(int classId, int subjectId)
        {
            return this.context.ClassesSubjects.Find(classId, subjectId);
        }

        public IEnumerable<ClassSubjectDAO> GetAll()
        {
            return this.context.ClassesSubjects;
        }

        public ClassSubjectDAO Update(ClassSubjectDAO tObjectChanges)
        {
            var tObject = this.context.ClassesSubjects.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
