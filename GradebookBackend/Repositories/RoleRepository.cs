﻿using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly GradebookDbContext context;

        public RoleRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public Role Add(Role tObject)
        {
            this.context.Roles.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public Role Delete(int Id)
        {
            Role tObject = this.context.Roles.Find(Id);
            if (tObject != null)
            {
                this.context.Roles.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public Role Get(int Id)
        {
            return this.context.Roles.Find(Id);
        }

        public IEnumerable<Role> GetAll()
        {
            return this.context.Roles;
        }

        public Role Update(Role tObjectChanges)
        {
            var tObject = this.context.Roles.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}