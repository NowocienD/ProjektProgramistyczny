using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradebookBackend.Model;

namespace GradebookBackend.Repositories
{
    public class LessonRepository : IRepository<Lesson>
    {
        private readonly GradebookDbContext context;

        public LessonRepository(GradebookDbContext context)
        {
            this.context = context;
        }

        public Lesson Add(Lesson lesson)
        {
            this.context.Lessons.Add(lesson);
            this.context.SaveChanges();
            return lesson;
        }

        public Lesson Delete(int Id)
        {
            Lesson lesson = this.context.Lessons.Find(Id);
            if (lesson != null)
            {
                this.context.Lessons.Remove(lesson);
                this.context.SaveChanges();
            }

            return lesson;
        }

        public Lesson Get(int Id)
        {
            return this.context.Lessons.Find(Id);
        }

        public IEnumerable<Lesson> GetAll()
        {
            return this.context.Lessons;
        }

        public Lesson Update(Lesson lessonChanges)
        {
            var lesson = this.context.Lessons.Attach(lessonChanges);
            lesson.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            this.context.SaveChanges();
            return lessonChanges;
        }
    }
}
