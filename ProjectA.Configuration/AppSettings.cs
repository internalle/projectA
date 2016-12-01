using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Configuration
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"];
    }
}
