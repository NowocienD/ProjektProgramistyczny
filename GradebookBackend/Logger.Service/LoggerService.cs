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

        public LogerService(ILogger<LogerService> logger)
        {
            this.logger = logger;
        }

        public void Debug(string message, object senderData)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(senderData);
            logger.LogDebug(message + data);
        }

        public void Info(string message, object senderData)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(senderData);
            logger.LogInformation(message + data);
        }

        public void Warning(string message, object senderData)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(senderData);
            logger.LogWarning(message + data);
        }

        public void Error(string message, object senderData)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(senderData);
            logger.LogError(message + data);
        }

        public void Critical(string message, object senderData)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(senderData);
            logger.LogCritical(message + data);
        }
    }
}

