using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradebookBackend.Model;

namespace GradebookBackend.Repositories
{
    public class ClassRepository : IRepository<Class>
    {
        private readonly GradebookDbContext context;

        public ClassRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public Class Add(Class tObject)
        {
            this.context.Classes.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public Class Delete(int Id)
        {
            Class tObject = this.context.Classes.Find(Id);
            if (tObject != null)
            {
                this.context.Classes.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public Class Get(int Id)
        {
            return this.context.Classes.Find(Id);
        }

        public IEnumerable<Class> GetAll()
        {
            return this.context.Classes;
        }

        public Class Update(Class tObjectChanges)
        {
            var tObject = this.context.Classes.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
