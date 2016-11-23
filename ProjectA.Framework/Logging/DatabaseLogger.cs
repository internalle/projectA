using ProjectA.Core.Infrastructure;
using ProjectA.Core.Infrastructure.Logging;
using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Framework.Logging
{
    public class DatabaseLogger : ILogger
    {
        MongoRepository<LoggedException> _exceptionRepo;
        MongoRepository<LoggedMessage> _messageRepo;

        public DatabaseLogger(MongoRepository<LoggedException> exceptionRepo,
                            MongoRepository<LoggedMessage> messageRepo)
        {
            _exceptionRepo = exceptionRepo;
            _messageRepo = messageRepo;
        }

        public void Error(Exception ex)
        {
            _exceptionRepo.Add(LoggedException.FromException(ex));
        }

        public void Info(string title, string message)
        {
            _messageRepo.Add(new LoggedMessage(title, message, LoggedMessage.LogType.Info));
        }

        public void Log(string title, string message)
        {
            _messageRepo.Add(new LoggedMessage(title, message, LoggedMessage.LogType.Log));
        }

        public void Warning(string title, string message)
        {
            _messageRepo.Add(new LoggedMessage(title, message, LoggedMessage.LogType.Warning));
        }
    }
}
