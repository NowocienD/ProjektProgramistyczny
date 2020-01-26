using GradebookBackend.Model;
using System.Collections.Generic;

namespace GradebookBackend.Repositories
{
    public class NoteRepository : IRepository<NoteDAO>
    {
        private readonly GradebookDbContext context;

        public NoteRepository(GradebookDbContext context)
        {
            this.context = context;
        }
        public NoteDAO Add(NoteDAO tObject)
        {
            this.context.Notes.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public NoteDAO Delete(int Id)
        {
            NoteDAO tObject = this.context.Notes.Find(Id);
            if (tObject != null)
            {
                this.context.Notes.Remove(tObject);
                this.context.SaveChanges();
            }

            return tObject;
        }

        public NoteDAO Get(int Id)
        {
            return this.context.Notes.Find(Id);
        }

        public IEnumerable<NoteDAO> GetAll()
        {
            return this.context.Notes;
        }

        public NoteDAO Update(NoteDAO tObjectChanges)
        {
            var tObject = this.context.Notes.Attach(tObjectChanges);
            tObject.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
