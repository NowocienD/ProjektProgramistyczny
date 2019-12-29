using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend
{
    public class GradebookServerException : Exception
    {
        public GradebookServerException(string statement) : base(statement)
        {

        }
    }
}
