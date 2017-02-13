using ProjectA.Configuration.Base.Enums;
using System.Configuration;

namespace ProjectA.Configuration.Base
{
    public class AppSettings : IAppSettings
    {
        public string MySqlConnectionString => ConfigurationManager.AppSettings["MySqlConnectionString"];

        public string MongoDBConnectionString => ConfigurationManager.AppSettings["MongoDBConnectionString"];

        public DatabaseType Database => DatabaseTypeHelper.FromString(ConfigurationManager.AppSettings["Database"]);

        public bool IsDebug => bool.Parse(ConfigurationManager.AppSettings["IsDebug"]);
    }
}
