using GradebookBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public class AttendanceStatusRepository : IRepository<AttendanceStatusDAO>
    {
        private readonly GradebookDbContext context;

        public AttendanceStatusRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public AttendanceStatusDAO Add(AttendanceStatusDAO tObject)
        {
            this.context.AttendancesStatus.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public AttendanceStatusDAO Delete(int Id)
        {
            AttendanceStatusDAO tObject = this.context.AttendancesStatus.Find(Id);
            if (tObject != null)
            {
                this.context.AttendancesStatus.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public AttendanceStatusDAO Get(int Id)
        {
            return this.context.AttendancesStatus.Find(Id);
        }

        public IEnumerable<AttendanceStatusDAO> GetAll()
        {
            return this.context.AttendancesStatus;
        }

        public AttendanceStatusDAO Update(AttendanceStatusDAO tObjectChanges)
        {
            var tObject = this.context.AttendancesStatus.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
