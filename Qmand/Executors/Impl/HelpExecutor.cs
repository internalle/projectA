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
            Marshal.Output.Invoke("");
            var commandOrExecutor = line.GetRest().GetFirst();

            if (!string.IsNullOrEmpty(commandOrExecutor))
            {
                try
                {
                    HelpForCommand(line);
                }
                catch
                {
                    HelpForExecutor(commandOrExecutor);
                }
            }
            else
            {
                GeneralHelp();
            }
            Marshal.Output.Invoke("");
        }

        private void GeneralHelp()
        {
            Marshal.Output.Invoke("List of executors:");
            foreach (var executor in Marshal.Executors)
            {
                Marshal.Output.Invoke(executor.Key);
            }
            Marshal.Output.Invoke("");
            Marshal.Output.Invoke("help <executor>, for more help for a specific executor");

            Marshal.Output.Invoke("");
            Marshal.Output.Invoke("");

            Marshal.Output.Invoke("List of commands:");
            foreach (var command in Marshal.Commands)
            {
                Marshal.Output.Invoke(command.Key);
            }
            Marshal.Output.Invoke("");
            Marshal.Output.Invoke("help <command>, for more help for a specific command");
        }

        private void HelpForCommand(string line)
        {
            var commandInstance = GetConsoleCommand(line);
            
            Marshal.Output.Invoke($"{commandInstance.Description}");

            Marshal.Output.Invoke("");
            Marshal.Output.Invoke("Example:");

            string commandExample = commandInstance.Name;

            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Required))
            {
                commandExample += $" --{requiredParam.Name} <Value>";
            }

            foreach (var optionalParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Optional))
            {
                commandExample += $" --{optionalParam.Name} <Value>";
            }

            Marshal.Output.Invoke(commandExample);
            Marshal.Output.Invoke("");
            foreach (var requiredParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Required))
            {
                Marshal.Output.Invoke($"--{requiredParam.Name} is requred, {requiredParam.Description}");
            }

            foreach (var optionalParam in commandInstance.ParametersDefinition.Where(x => x.Type == ParameterType.Optional))
            {
                Marshal.Output.Invoke($"--{optionalParam.Name} is optional, { optionalParam.Description}");
            }
        }

        private void HelpForExecutor(string exectorName)
        {
            var executorInstance = Activator.CreateInstance(Marshal.Executors[exectorName.GetFirst()]) as Executor;
            
            Marshal.Output.Invoke($"{executorInstance.Description}");
        }
    }
}
