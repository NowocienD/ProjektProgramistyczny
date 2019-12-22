using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class SubjectRepository : IRepository<SubjectDAO>
    {
        private readonly GradebookDbContext context;

        public SubjectRepository(GradebookDbContext context)
        {
            this.context = context;
        }

        public SubjectDAO Add(SubjectDAO subject)
        {
            this.context.Subjects.Add(subject);
            this.context.SaveChanges();
            return subject;
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

        public SubjectDAO Update(SubjectDAO subjectChanges)
        {
            var subject = this.context.Subjects.Attach(subjectChanges);
            subject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return subjectChanges;
        }
    }
}
