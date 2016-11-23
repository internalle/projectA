using MongoRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core.Infrastructure.Logging
{
    public class LoggedException : Entity
    {
        public IDictionary Data { get; set; }

        public string HelpLink { get; set; }

        public int HResult { get; set; }

        public LoggedException InnerException { get; set; }

        public string Message { get; set; }

        public string Source { get; set; }

        public string StackTrace { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public static LoggedException FromException(Exception ex)
        {
            if (ex == null)
                return null;

            return new LoggedException
            {
                Data = ex.Data,
                HelpLink = ex.HelpLink,
                HResult = ex.HResult,
                Source = ex.Source,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                CreatedOn = DateTime.UtcNow,
                InnerException = FromException(ex.InnerException)
            };
        }
    }
}
