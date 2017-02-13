using Qmand.Commands.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qmand.Executors.Impl
{
    public class HelpExecutor : BaseExecutor
    {
        protected override string ExecutorName => "help";

        protected override bool ShouldHaveCommand => true;

        public override void Execute(string command)
        {
            var commandInstance = GetConsoleCommand(command);

            Output.Invoke("");
            Output.Invoke($"{commandInstance.Description}");

            Output.Invoke("");
            Output.Invoke("Example:");
            Output.Invoke($"{commandInstance.Name}");

            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Required))
            {
                Output.Invoke($" --{requiredParam.Name} <Value>");
            }

            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Optional))
            {
                Output.Invoke($" --{requiredParam.Name} <Value>");
            }

            Output.Invoke("");
            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Required))
            {
                Output.Invoke("");
                Output.Invoke($"--{requiredParam.Name} is requred");
                Output.Invoke($"{requiredParam.Description}");
            }

            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Optional))
            {
                Output.Invoke("");
                Output.Invoke($"--{requiredParam.Name} is optional");
                Output.Invoke($"{requiredParam.Description}");
            }
        }
    }
}
