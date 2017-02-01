using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core.Infrastructure.Logging
{
    public class LoggedMessage : ActiveRecord<LoggedMessage>
    {
        public LoggedMessage()
        {
        }

        public LoggedMessage(string title, string message, LogType type)
        {
            Title = title;
            Message = message;
            Type = type;
        }

        public virtual string Title { get; set; }

        public virtual string Message { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual LogType Type { get; set; }
        
        public enum LogType
        {
            Log, Info, Warning
        }
    }
}
