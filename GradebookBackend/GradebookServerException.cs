using System;

namespace GradebookBackend
{
    public class GradebookServerException : Exception
    {
        public GradebookServerException(string statement) : base(statement)
        {

        }
    }
}
