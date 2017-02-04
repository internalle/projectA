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
        public override string Description => "Runs one specific seeder or every registered seeder";

        public override string Name => "seed";

        public override Dictionary<string, Tuple<ParameterType, string>> ParametersDefinition => new Dictionary<string, Tuple<ParameterType, string>>
        {
            { "class", new Tuple<ParameterType, string>(ParameterType.Optional, "A Type of specific seeder")}
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
