using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradebookBackend.Model;

namespace GradebookBackend.Repositories
{
    public class NoteRepository : IRepository<Note>
    {
        private readonly GradebookDbContext context;

        public NoteRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public Note Add(Note tObject)
        {
            this.context.Notes.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public Note Delete(int Id)
        {
            Note tObject = this.context.Notes.Find(Id);
            if (tObject != null)
            {
                this.context.Notes.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public Note Get(int Id)
        {
            return this.context.Notes.Find(Id);
        }

        public IEnumerable<Note> GetAll()
        {
            return this.context.Notes;
        }

        public Note Update(Note tObjectChanges)
        {
            var tObject = this.context.Notes.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
