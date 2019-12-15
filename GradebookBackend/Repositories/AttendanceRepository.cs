using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradebookBackend.Model;

namespace GradebookBackend.Repositories
{
    public class AttendanceRepository : IRepository<Attendance>
    {
        private readonly GradebookDbContext context;

        public AttendanceRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public Attendance Add(Attendance tObject)
        {
            this.context.Attendances.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public Attendance Delete(int Id)
        {
            Attendance tObject = this.context.Attendances.Find(Id);
            if (tObject != null)
            {
                this.context.Attendances.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public Attendance Get(int Id)
        {
            return this.context.Attendances.Find(Id);
        }

        public IEnumerable<Attendance> GetAll()
        {
            return this.context.Attendances;
        }

        public Attendance Update(Attendance tObjectChanges)
        {
            var tObject = this.context.Attendances.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
