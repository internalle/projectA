using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Configuration
{
    public interface IAppSettings
    {
        string MongoDBConnectionString { get; }
    }
}
