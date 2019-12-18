using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradebookBackend.Model;

namespace GradebookBackend.Repositories
{
    public class StudentRepository :IRepository<Student>
    {
        private readonly GradebookDbContext context;

        public StudentRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public Student Add(Student tObject)
        {
            this.context.Students.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public Student Delete(int Id)
        {
            Student tObject = this.context.Students.Find(Id);
            if (tObject != null)
            {
                this.context.Students.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public Student Get(int Id)
        {
            return this.context.Students.Find(Id);
        }

        public IEnumerable<Student> GetAll()
        {
            return this.context.Students;
        }

        public Student Update(Student tObjectChanges)
        {
            var tObject = this.context.Students.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
