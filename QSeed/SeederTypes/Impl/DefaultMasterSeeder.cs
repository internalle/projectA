using QSeed.Config;
using QSeed.Model;
using QSeed.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QSeed.SeederTypes.Impl
{
    internal class DefaultMasterSeeder : MasterSeeder
    {
        public override void Run()
        {
            foreach (var seeder in _factory.SeederTypes)
            {
                RunSeeder(seeder);
            }
        }
    }
}
