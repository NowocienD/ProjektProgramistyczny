using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradebookBackend.Model;

namespace GradebookBackend.Repositories
{
    public class ClassRepository : IRepository<ClassDAO>
    {
        private readonly GradebookDbContext context;

        public ClassRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public ClassDAO Add(ClassDAO tObject)
        {
            this.context.Classes.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public ClassDAO Delete(int Id)
        {
            ClassDAO tObject = this.context.Classes.Find(Id);
            if (tObject != null)
            {
                this.context.Classes.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public ClassDAO Get(int Id)
        {
            return this.context.Classes.Find(Id);
        }

        public IEnumerable<ClassDAO> GetAll()
        {
            return this.context.Classes;
        }

        public ClassDAO Update(ClassDAO tObjectChanges)
        {
            var tObject = this.context.Classes.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
