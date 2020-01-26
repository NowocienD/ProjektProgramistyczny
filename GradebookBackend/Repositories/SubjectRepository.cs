using GradebookBackend.Model;
using System.Collections.Generic;

namespace GradebookBackend.Repositories
{
    public class SubjectRepository : IRepository<SubjectDAO>
    {
        private readonly GradebookDbContext context;

        public SubjectRepository(GradebookDbContext context)
        {
            this.context = context;
        }

        public SubjectDAO Add(SubjectDAO tObject)
        {
            this.context.Subjects.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public SubjectDAO Delete(int Id)
        {
            SubjectDAO subject = this.context.Subjects.Find(Id);
            if (subject != null)
            {
                this.context.Subjects.Remove(subject);
                this.context.SaveChanges();
            }

            return subject;
        }

        public SubjectDAO Get(int Id)
        {
            return this.context.Subjects.Find(Id);
        }

        public IEnumerable<SubjectDAO> GetAll()
        {
            return this.context.Subjects;
        }

        public SubjectDAO Update(SubjectDAO tObjectChanges)
        {
            context.Entry(tObjectChanges).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            var subject = this.context.Subjects.Attach(tObjectChanges);
            subject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
