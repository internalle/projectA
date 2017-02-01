using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core.Infrastructure.Logging
{
    public class LoggedException : ActiveRecord<LoggedException>
    {
        public virtual LoggedException InnerException { get; set; }

        public virtual string Message { get; set; }

        public virtual string Source { get; set; }

        public virtual string StackTrace { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public static LoggedException FromException(Exception ex)
        {
            if (ex == null)
                return null;

            return new LoggedException
            {
                Source = ex.Source,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                CreatedOn = DateTime.UtcNow,
                InnerException = FromException(ex.InnerException)
            };
        }
    }

    //public class LoggedExcaptionMap : IAutoMappingOverride<LoggedException>
    //{
    //    public void Override(AutoMapping<LoggedException> mapping)
    //    {
    //        mapping.IgnoreProperty(x => x.Data);
    //    }
    //}
}
