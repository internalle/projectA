using MongoRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core.Infrastructure.Logging
{
    public class LoggedMessage : Entity
    {
        public LoggedMessage(string title, string message, LogType type)
        {
            Title = title;
            Message = message;
            Type = type;
        }

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime CreatedOn { get; set; }

        public LogType Type { get; set; }
        
        public enum LogType
        {
            Log, Info, Warning
        }
    }
}
