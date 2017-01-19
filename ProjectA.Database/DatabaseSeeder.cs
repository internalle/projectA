using ProjectA.Seeders.Features.Measurements;
using QSeed;
using QSeed.SeederTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QSeed.Config;

namespace ProjectA.Database
{
    public class DatabaseSeeder : MasterSeeder
    {
        public override void Run()
        {
            RunSeeder(typeof(MeasuremetUnitSeeder));
        }
    }
}
