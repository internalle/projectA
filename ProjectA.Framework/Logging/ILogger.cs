using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Framework.Logging
{
    public interface ILogger
    {
        void Error(Exception ex);

        void Info(string title, string message);

        void Warning(string title, string message);

        void Log(string title, string message);
    }
}
