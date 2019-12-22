using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class RoleRepository : IRepository<RoleDAO>
    {
        private readonly GradebookDbContext context;

        public RoleRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public RoleDAO Add(RoleDAO tObject)
        {
            this.context.Roles.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public RoleDAO Delete(int Id)
        {
            RoleDAO tObject = this.context.Roles.Find(Id);
            if (tObject != null)
            {
                this.context.Roles.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public RoleDAO Get(int Id)
        {
            return this.context.Roles.Find(Id);
        }

        public IEnumerable<RoleDAO> GetAll()
        {
            return this.context.Roles;
        }

        public RoleDAO Update(RoleDAO tObjectChanges)
        {
            var tObject = this.context.Roles.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
