using GradebookBackend.Model;
using System.Collections.Generic;

namespace GradebookBackend.Repositories
{
    public class LessonRepository : IRepository<LessonDAO>
    {
        private readonly GradebookDbContext context;

        public LessonRepository(GradebookDbContext context)
        {
            this.context = context;
        }

        public LessonDAO Add(LessonDAO tObject)
        {
            this.context.Lessons.Add(tObject);
            this.context.SaveChanges();
            return tObject;
        }

        public LessonDAO Delete(int Id)
        {
            LessonDAO lesson = this.context.Lessons.Find(Id);
            if (lesson != null)
            {
                this.context.Lessons.Remove(lesson);
                this.context.SaveChanges();
            }

            return lesson;
        }

        public LessonDAO Get(int Id)
        {
            return this.context.Lessons.Find(Id);
        }

        public IEnumerable<LessonDAO> GetAll()
        {
            return this.context.Lessons;
        }

        public LessonDAO Update(LessonDAO tObjectChanges)
        {
            context.Entry(tObjectChanges).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            var lesson = this.context.Lessons.Attach(tObjectChanges);
            lesson.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return tObjectChanges;
        }
    }
}
