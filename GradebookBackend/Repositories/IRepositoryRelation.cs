using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    public interface IRepositoryRelation<T> where T : class
    {
        T Get(int firstId, int secondId);

        IEnumerable<T> GetAll();

        T Delete(int firstId, int secondId);

        T Add(T tObject);

        T Update(T tObject);
    }
}
