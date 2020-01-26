using System.Collections.Generic;

namespace GradebookBackend.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(int Id);

        IEnumerable<T> GetAll();

        T Delete(int Id);

        T Add(T tObject);

        T Update(T tObject);
    }
}
