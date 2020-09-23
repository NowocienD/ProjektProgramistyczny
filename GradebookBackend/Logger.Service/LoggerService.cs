using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;

namespace GradebookBackend.Logger.Service
{
    public class LogerService : ILogerService
    {
        private readonly ILogger logger;

        public LogerService(ILogger logger)
        {
            this.logger = logger;
        }

        public void Debug(string message)
        {
            logger.LogDebug(message);
        }

        public void Info(string message)
        {
            logger.LogInformation(message);
        }

        public void Warning(string message)
        {
            logger.LogWarning(message);
        }

        public void Error(string message)
        {
            logger.LogError(message);
        }

        public void Critical(string message)
        {
            logger.LogCritical(message);
        }
    }
}

