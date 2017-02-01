using ProjectA.Configuration.Base.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Configuration.Base
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"];

        public DatabaseType Database => DatabaseTypeHelper.FromString(ConfigurationManager.AppSettings["Database"]);

        public bool IsDebug => bool.Parse(ConfigurationManager.AppSettings["IsDebug"]);
    }
}
