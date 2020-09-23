using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Logger.Service
{
    public interface ILogerService
    {
        void Debug(string message, object senderData);

        void Info(string message, object senderData);

        void Warning(string message, object senderData);

        void Error(string message, object senderData);

        void Critical(string message, object senderData);
    }
}
