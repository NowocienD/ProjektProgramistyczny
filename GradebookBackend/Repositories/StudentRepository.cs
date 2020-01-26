using GradebookBackend.Model;
using System.Collections.Generic;

namespace GradebookBackend.Repositories
{
    public class StudentRepository : IRepository<StudentDAO>
    {
        private readonly GradebookDbContext context;

        public StudentRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public StudentDAO Add(StudentDAO tObject)
        {
            this.context.Students.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public StudentDAO Delete(int Id)
        {
            StudentDAO tObject = this.context.Students.Find(Id);
            if (tObject != null)
            {
                this.context.Students.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public StudentDAO Get(int Id)
        {
            return this.context.Students.Find(Id);
        }

        public IEnumerable<StudentDAO> GetAll()
        {
            return this.context.Students;
        }

        public StudentDAO Update(StudentDAO tObjectChanges)
        {
            context.Entry(tObjectChanges).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            var tObject = this.context.Students.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
