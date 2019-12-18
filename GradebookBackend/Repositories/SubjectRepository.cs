using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class SubjectRepository : IRepository<Subject>
    {
        private readonly GradebookDbContext context;

        public SubjectRepository(GradebookDbContext context)
        {
            this.context = context;
        }

        public Subject Add(Subject subject)
        {
            this.context.Subjects.Add(subject);
            this.context.SaveChanges();
            return subject;
        }

        public Subject Delete(int Id)
        {
            Subject subject = this.context.Subjects.Find(Id);
            if (subject != null)
            {
                this.context.Subjects.Remove(subject);
                this.context.SaveChanges();
            }

            return subject;
        }

        public Subject Get(int Id)
        {
            return this.context.Subjects.Find(Id);
        }

        public IEnumerable<Subject> GetAll()
        {
            return this.context.Subjects;
        }

        public Subject Update(Subject subjectChanges)
        {
            var subject = this.context.Subjects.Attach(subjectChanges);
            subject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return subjectChanges;
        }
    }
}
