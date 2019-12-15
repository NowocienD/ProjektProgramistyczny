using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class TeacherRepository : IRepository<Teacher>
    {
        private readonly GradebookDbContext context;

        public TeacherRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public Teacher Add(Teacher tObject)
        {
            this.context.Teachers.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public Teacher Delete(int Id)
        {
            Teacher tObject = this.context.Teachers.Find(Id);
            if (tObject != null)
            {
                this.context.Teachers.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public Teacher Get(int Id)
        {
            return this.context.Teachers.Find(Id);
        }

        public IEnumerable<Teacher> GetAll()
        {
            return this.context.Teachers;
        }

        public Teacher Update(Teacher tObjectChanges)
        {
            var tObject = this.context.Teachers.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
