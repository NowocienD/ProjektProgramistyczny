using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class UserRepository : IRepository<UserDAO>
    {
        private readonly GradebookDbContext context;

        public UserRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public UserDAO Add(UserDAO tObject)
        {
            this.context.Users.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public UserDAO Delete(int Id)
        {
            UserDAO tObject = this.context.Users.Find(Id);
            if (tObject != null)
            {
                this.context.Users.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public UserDAO Get(int Id)
        {
            return this.context.Users.Find(Id);
        }

        public IEnumerable<UserDAO> GetAll()
        {
            return this.context.Users;
        }

        public UserDAO Update(UserDAO tObjectChanges)
        {
            context.Entry(tObjectChanges).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            var tObject = this.context.Users.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
