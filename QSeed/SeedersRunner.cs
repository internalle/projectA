using QSeed.Config;
using QSeed.Model;
using QSeed.ReflectionExtensions;
using QSeed.SeederTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QSeed
{
    public class SeedersRunner
    {
        private Configuration _conf { get; set; }
        private List<Type> _seederTypes { get; set; }
        private List<Type> _factoryTypes { get; set; }

        public SeedersRunner(Type repositoryImplType, Type masterSeederType = default(Type))
        {
            _seederTypes = new List<Type>();
            _factoryTypes = new List<Type>();

            _conf = new Configuration
            {
                Repository = repositoryImplType,
                MasterSeeder = masterSeederType,
                BaseSeeders = _seederTypes,
                ModalFactories = _factoryTypes
            };
        }

        public SeedersRunner RegisterSeedersAssembly(Assembly assembly)
        {
            var scannedSeeders = assembly.GetExportedTypes().Where(x => x.IsBaseSeeder());
            foreach(var seeder in scannedSeeders)
            {
                RegisterSeederType(seeder);
            }

            return this;
        }

        public SeedersRunner RegisterSeederType(Type seederType)
        {
            if (seederType == default(Type) || !seederType.IsBaseSeeder())
            {
                throw new InvalidCastException($"{typeof(BaseSeeder).FullName} expected");
            }

            _seederTypes.Add(seederType);
            return this;
        }

        public SeedersRunner RegisterFactoriesAssembly(Assembly assembly)
        {
            var scannedModelFactories = assembly.GetModelFactoryTypes();
            foreach (var factory in scannedModelFactories)
            {
                RegisterFactoryType(factory);
            }

            return this;
        }

        public SeedersRunner RegisterFactoryType(Type factoryType)
        {
            if (factoryType == default(Type) || !factoryType.IsModelFactory())
            {
                throw new InvalidCastException($"{typeof(ModelFactory<>).FullName} expected");
            }

            _factoryTypes.Add(factoryType);
            return this;
        }

        public void Run()
        {
            _seederTypes = _seederTypes.Distinct().ToList();
            _factoryTypes = _factoryTypes.Distinct().ToList();

            var _factory = FactoryAndSeederFactory.FromConfiguration(_conf);

            _factory.GetMasterSeederInstance().Run();
        }
    }
}
