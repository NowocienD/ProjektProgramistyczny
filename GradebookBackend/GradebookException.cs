using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend
{
    public class GradebookException : Exception
    {
        public GradebookException(string statement) : base(statement)
        {

        }
    }
}
