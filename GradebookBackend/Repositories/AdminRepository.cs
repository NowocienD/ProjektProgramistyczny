using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class AdminRepository : IRepository<AdminDAO>
    {
        private readonly GradebookDbContext context;

        public AdminRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public AdminDAO Add(AdminDAO tObject)
        {
            this.context.Admins.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public AdminDAO Delete(int Id)
        {
            AdminDAO tObject = this.context.Admins.Find(Id);
            if (tObject != null)
            {
                this.context.Admins.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public AdminDAO Get(int Id)
        {
            return this.context.Admins.Find(Id);
        }

        public IEnumerable<AdminDAO> GetAll()
        {
            return this.context.Admins;
        }

        public AdminDAO Update(AdminDAO tObjectChanges)
        {
            var tObject = this.context.Admins.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
