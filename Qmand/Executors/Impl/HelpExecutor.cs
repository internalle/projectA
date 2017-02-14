using QMand.Commands.Definition;
using QMand.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMand.Executors.Impl
{
    public class HelpExecutor : Executor
    {
        public override string Name => "help";

        public override string Description => 
@"Display general or specific help for commands and executors

help [<CommandOrExecutorName>]";

        public override void Execute(string line)
        {
            Output.Invoke("");
            if (!string.IsNullOrEmpty(line.GetCommandName()))
            {
                try
                {
                    HelpForCommand(line);
                }
                catch
                {
                    HelpForExecutor(line);
                }
            }
            else
            {
                GeneralHelp();
            }
            Output.Invoke("");
        }

        private void GeneralHelp()
        {
            Output.Invoke("List of executors:");
            foreach (var executor in Executors)
            {
                Output.Invoke(executor.Key);
            }
            Output.Invoke("");
            Output.Invoke("help <executor>, for more help for a specific executor");

            Output.Invoke("");
            Output.Invoke("");

            Output.Invoke("List of commands:");
            foreach (var command in Commands)
            {
                Output.Invoke(command.Key);
            }
            Output.Invoke("");
            Output.Invoke("help <command>, for more help for a specific command");
        }

        private void HelpForCommand(string line)
        {
            var commandInstance = GetConsoleCommand(line);
            
            Output.Invoke($"{commandInstance.Description}");

            Output.Invoke("");
            Output.Invoke("Example:");

            string commandExample = commandInstance.Name;

            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Required))
            {
                commandExample += $" --{requiredParam.Name} <Value>";
            }

            foreach (var optionalParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Optional))
            {
                commandExample += $" --{optionalParam.Name} <Value>";
            }

            Output.Invoke(commandExample);
            Output.Invoke("");
            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Required))
            {
                Output.Invoke($"--{requiredParam.Name} is requred, {requiredParam.Description}");
            }

            foreach (var optionalParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Optional))
            {
                Output.Invoke($"--{optionalParam.Name} is optional, { optionalParam.Description}");
            }
        }

        private void HelpForExecutor(string line)
        {
            var commandInstance = GetExecutor(line);
            
            Output.Invoke($"{commandInstance.Description}");
        }
    }
}
