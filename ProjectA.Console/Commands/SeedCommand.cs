using ProjectA.Database;
using Qmand;
using QSeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Console.Commands
{
    public class SeedCommand : ConsoleCommand
    {
        public override string Description => "Seed command";

        public override string Name => "seed";

        public override Dictionary<string, ParameterType> ParametersDefinition => new Dictionary<string, ParameterType>
        {
            { "class", ParameterType.Optional }
        };

        public override void Run()
        {
            var seedClassName = Parameters.ContainsKey("class") ? Parameters["class"] : null;

            if (string.IsNullOrEmpty(seedClassName))
            {
                new SeedersRunner(typeof(Repository<>), masterSeederType: typeof(DatabaseSeeder))
                    .RegisterSeedersAssembly(typeof(DatabaseSeeder).Assembly)
                    .RegisterFactoriesAssembly(typeof(DatabaseSeeder).Assembly)
                    .Run();
            }else
            {
                var seederType = typeof(DatabaseSeeder).Assembly.GetExportedTypes().FirstOrDefault(x=>x.Name == seedClassName);
                new SeedersRunner(typeof(Repository<>))
                    .RegisterSeederType(seederType)
                    .RegisterFactoriesAssembly(typeof(DatabaseSeeder).Assembly)
                    .Run();
            }
        }
    }
}
