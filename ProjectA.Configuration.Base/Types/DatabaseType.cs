using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Configuration.Base.Types
{
    public enum DatabaseType
    {
        MySQL, MsSQL, PostgreSQL, MongoDB
    }

    public static class DatabaseTypeHelper
    {
        public static DatabaseType FromString(string dbType)
        {
            switch (dbType)
            {
                case "MySQL":
                    return DatabaseType.MySQL;
                case "MsSQL":
                    return DatabaseType.MsSQL;
                case "PostgreSQL":
                    return DatabaseType.PostgreSQL;
                case "MongoDB":
                    return DatabaseType.MongoDB;
                default:
                    return DatabaseType.MySQL;
            };
        }
    }
}
