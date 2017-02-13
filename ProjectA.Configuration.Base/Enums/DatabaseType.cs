using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Configuration.Base.Enums
{
    public enum DatabaseType
    {
        MySQL, MsSQL, PostgreSQL, MongoDB
    }

    public static class DatabaseTypeHelper
    {
        public static DatabaseType FromString(string dbType)
        {
            switch (dbType.ToLowerInvariant())
            {
                case "mysql":
                    return DatabaseType.MySQL;
                case "mssql":
                    return DatabaseType.MsSQL;
                case "postgresql":
                    return DatabaseType.PostgreSQL;
                case "mongodb":
                    return DatabaseType.MongoDB;
                default:
                    return DatabaseType.MySQL;
            };
        }
    }
}
