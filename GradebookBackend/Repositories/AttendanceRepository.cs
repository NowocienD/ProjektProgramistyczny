using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradebookBackend.Model;

namespace GradebookBackend.Repositories
{
    public class AttendanceRepository : IRepository<AttendanceDAO>
    {
        private readonly GradebookDbContext context;

        public AttendanceRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public AttendanceDAO Add(AttendanceDAO tObject)
        {
            this.context.Attendances.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public AttendanceDAO Delete(int Id)
        {
            AttendanceDAO tObject = this.context.Attendances.Find(Id);
            if (tObject != null)
            {
                this.context.Attendances.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public AttendanceDAO Get(int Id)
        {
            return this.context.Attendances.Find(Id);
        }

        public IEnumerable<AttendanceDAO> GetAll()
        {
            return this.context.Attendances;
        }

        public AttendanceDAO Update(AttendanceDAO tObjectChanges)
        {
            var tObject = this.context.Attendances.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
