using ProjectA.Core.Infrastructure;
using ProjectA.Core.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Framework.Logging
{
    public class DatabaseLogger : ILogger
    {
        public void Error(Exception ex)
        {
            LoggedException.FromException(ex).Save();
        }

        public void Info(string title, string message)
        {
            new LoggedMessage(title, message, LoggedMessage.LogType.Info).Save();
        }

        public void Log(string title, string message)
        {
            new LoggedMessage(title, message, LoggedMessage.LogType.Log).Save();
        }

        public void Warning(string title, string message)
        {
            new LoggedMessage(title, message, LoggedMessage.LogType.Warning).Save();
        }
    }
}
