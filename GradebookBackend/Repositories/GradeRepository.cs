using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradebookBackend.Model;

namespace GradebookBackend.Repositories
{
    public class GradeRepository : IRepository<Grade>
    {
        private readonly GradebookDbContext context;

        public GradeRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public Grade Add(Grade tObject)
        {
            this.context.Grades.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public Grade Delete(int Id)
        {
            Grade tObject = this.context.Grades.Find(Id);
            if (tObject != null)
            {
                this.context.Grades.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public Grade Get(int Id)
        {
            return this.context.Grades.Find(Id);
        }

        public IEnumerable<Grade> GetAll()
        {
            return this.context.Grades;
        }

        public Grade Update(Grade tObjectChanges)
        {
            var tObject = this.context.Grades.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
