using QSeed.Config;
using QSeed.Model;
using QSeed.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QSeed.SeederTypes
{
    public abstract class MasterSeeder : BaseSeeder
    {        
        public void RunSeeder(Type seederType)
        {
            var seeder = Activator.CreateInstance(seederType) as BaseSeeder;
            seeder.SetFactory(_factory);
            seeder.Run();
        }
    }
}
