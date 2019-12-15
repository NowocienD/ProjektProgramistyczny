using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class AdminRepository : IRepository<Admin>
    {
        private readonly GradebookDbContext context;

        public AdminRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public Admin Add(Admin tObject)
        {
            this.context.Admins.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public Admin Delete(int Id)
        {
            Admin tObject = this.context.Admins.Find(Id);
            if (tObject != null)
            {
                this.context.Admins.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public Admin Get(int Id)
        {
            return this.context.Admins.Find(Id);
        }

        public IEnumerable<Admin> GetAll()
        {
            return this.context.Admins;
        }

        public Admin Update(Admin tObjectChanges)
        {
            var tObject = this.context.Admins.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
