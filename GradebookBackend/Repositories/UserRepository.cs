using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly GradebookDbContext context;

        public UserRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public User Add(User tObject)
        {
            this.context.Users.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public User Delete(int Id)
        {
            User tObject = this.context.Users.Find(Id);
            if (tObject != null)
            {
                this.context.Users.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public User Get(int Id)
        {
            return this.context.Users.Find(Id);
        }

        public IEnumerable<User> GetAll()
        {
            return this.context.Users;
        }

        public User Update(User tObjectChanges)
        {
            var tObject = this.context.Users.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
