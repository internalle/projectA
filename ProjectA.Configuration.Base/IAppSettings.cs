using ProjectA.Configuration.Base.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Configuration.Base
{
    public interface IAppSettings
    {
        string ConnectionString { get; }

        DatabaseType Database { get; }

        bool IsDebug { get; }
    }
}
