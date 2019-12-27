using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class ClassSubjectRepository : IRepository<ClassSubjectDAO>
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

        public ClassSubjectDAO Delete(int Id)
        {
            ClassSubjectDAO tObject = this.context.ClassesSubjects.Find(Id);
            if (tObject != null)
            {
                this.context.ClassesSubjects.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public ClassSubjectDAO Get(int Id)
        {
            return this.context.ClassesSubjects.Find(Id);
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
