using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class TeacherRepository : IRepository<TeacherDAO>
    {
        private readonly GradebookDbContext context;

        public TeacherRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public TeacherDAO Add(TeacherDAO tObject)
        {
            this.context.Teachers.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public TeacherDAO Delete(int Id)
        {
            TeacherDAO tObject = this.context.Teachers.Find(Id);
            if (tObject != null)
            {
                this.context.Teachers.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public TeacherDAO Get(int Id)
        {
            return this.context.Teachers.Find(Id);
        }

        public IEnumerable<TeacherDAO> GetAll()
        {
            return this.context.Teachers;
        }

        public TeacherDAO Update(TeacherDAO tObjectChanges)
        {
            var tObject = this.context.Teachers.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
