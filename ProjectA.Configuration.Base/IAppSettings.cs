using ProjectA.Configuration.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Configuration.Base
{
    public interface IAppSettings
    {
        string MySqlConnectionString { get; }

        string MongoDBConnectionString { get; }

        DatabaseType Database { get; }

        bool IsDebug { get; }
    }
}
