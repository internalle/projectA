using ProjectA.Seeders.Features.Measurements;
using QSeed;
using QSeed.SeederTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QSeed.Config;
using ProjectA.Configuration.Base;
using Microsoft.Practices.ServiceLocation;

namespace ProjectA.Database
{
    public class DatabaseSeeder : MasterSeeder
    {
        private IAppSettings _settings => ServiceLocator.Current.GetInstance<IAppSettings>();

        public override void Run()
        {
            RunSeeder(typeof(MeasurementUnitSeeder));

            if (_settings.IsDebug)
            {
            }
        }
    }
}
