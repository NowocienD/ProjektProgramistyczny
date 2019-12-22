using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradebookBackend.Model;

namespace GradebookBackend.Repositories
{
    public class GradeRepository : IRepository<GradeDAO>
    {
        private readonly GradebookDbContext context;

        public GradeRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public GradeDAO Add(GradeDAO tObject)
        {
            this.context.Grades.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public GradeDAO Delete(int Id)
        {
            GradeDAO tObject = this.context.Grades.Find(Id);
            if (tObject != null)
            {
                this.context.Grades.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public GradeDAO Get(int Id)
        {
            return this.context.Grades.Find(Id);
        }

        public IEnumerable<GradeDAO> GetAll()
        {
            return this.context.Grades;
        }

        public GradeDAO Update(GradeDAO tObjectChanges)
        {
            var tObject = this.context.Grades.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
