using ProjectA.DatabaseSeeders;
using QMand;
using QMand.Commands;
using QSeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QMand.Commands.Definition;
using System.Reflection;

namespace ProjectA.Console.Commands
{
    public class SeedCommand : ConsoleCommand
    {
        public override string Description => "Runs one specific seeder or every registered seeder";

        public override string Name => "seeder";

        public override List<CommandParameter> ParametersDefinition => new List<CommandParameter>
        {
            new CommandParameter { Name = "class", Description = "A type of seeder", Type = ParameterType.Optional }
        };

        public override void Run()
        {
            var seedClassName = GetParametar("class");
            
            var repoType = typeof(Repository<>);
            var masterSeederType = typeof(DatabaseSeeder);
            var rootAssembly = masterSeederType.Assembly;

            SeedersRunner seederRunner = null;

            if (string.IsNullOrEmpty(seedClassName))
            {
                seederRunner = new SeedersRunner(repoType, masterSeederType: masterSeederType);
                seederRunner.RegisterSeedersAssembly(rootAssembly);
            }
            else
            {
                var seederType = GetSpecificSeeder(rootAssembly, seedClassName);
                seederRunner = new SeedersRunner(repoType);
                seederRunner.RegisterSeederType(seederType);
            }

            seederRunner
                .RegisterFactoriesAssembly(rootAssembly)
                .Run();
        }

        private Type GetSpecificSeeder(Assembly haystackAssembly, string seedClassName)
        {
            var result = haystackAssembly.GetExportedTypes().FirstOrDefault(x => x.Name == seedClassName);

            if (result == default(Type))
            {
                throw new NullReferenceException($"Seeder of type {seedClassName} does not exist");
            }

            return result;
        }
    }
}
