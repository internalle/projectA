using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qmand.Executors
{
    public class HelpExecutor : BaseExecutor
    {
        protected override string ExecutorName => "help";

        protected override bool ShouldHaveCommand => true;

        public override void Execute(string command)
        {
            var commandInstance = GetConsoleCommand(command);

            Console.WriteLine();
            Console.WriteLine($"{commandInstance.Description}");

            Console.WriteLine();
            Console.WriteLine("Example:");
            Console.Write($"{commandInstance.Name}");

            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Value.Item1 == ParameterType.Required))
            {
                Console.Write($" --{requiredParam.Key} <Value>");
            }

            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Value.Item1 == ParameterType.Optional))
            {
                Console.Write($" --{requiredParam.Key} <Value>");
            }

            Console.WriteLine();
            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Value.Item1 == ParameterType.Required))
            {
                Console.WriteLine();
                Console.WriteLine($"--{requiredParam.Key} is requred");
                Console.WriteLine($"{requiredParam.Value.Item2}");
            }

            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Value.Item1 == ParameterType.Optional))
            {
                Console.WriteLine();
                Console.WriteLine($"--{requiredParam.Key} is optional");
                Console.WriteLine($"{requiredParam.Value.Item2}");
            }
        }
    }
}
