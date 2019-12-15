using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Repositories
{
    interface IRepository<T> where T:class
    {
        T Get(int Id);

        IEnumerable<T> GetAll();

        T Delete(int Id);

        T Add(T tObject);

        T Update(T tObject);
    }
}
