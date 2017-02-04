using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qmand
{
    public abstract class BaseExecutor
    {
        protected abstract string ExecutorName { get; }

        protected abstract bool ShouldHaveCommand { get; }

        public abstract void Execute(string command);

        protected virtual ConsoleCommand GetConsoleCommand(string command)
        {
            var commandName = GetCommandName(command);
            var commandParams = GetCommandParameters(command);

            if (!CommandMarshal.Commands.ContainsKey(commandName))
            {
                throw new Exception("Unknown command");
            }

            var instance = Activator.CreateInstance(CommandMarshal.Commands[commandName]) as ConsoleCommand;

            if (!HasRequiredParameters(instance.ParametersDefinition, commandParams))
            {
                throw new Exception("Required parameters are not defined");
            }

            if (!HasUnknownParameters(instance.ParametersDefinition, commandParams))
            {
                throw new Exception("Some parameters are not defined");
            }

            instance.Parameters = commandParams;
            return instance;
        }

        internal bool IsForThisExecutor(string line)
        {
            return line.StartsWith($"{ExecutorName}") && (!ShouldHaveCommand || HasCommand(line));
        }

        private bool HasCommand(string line)
        {
            return !string.IsNullOrEmpty(line.Replace($"{ExecutorName}", "").Trim(new char[] { ' ' }));
        }

        private string GetCommandName(string line)
        {
            return line.Replace($"{ExecutorName}", "").Trim(new char[] { ' ' }).Split(' ')[0];
        }

        private Dictionary<string, string> GetCommandParameters(string line)
        {
            var parameters = new Dictionary<string, string>();

            var parameterParts = line.Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
            parameterParts = parameterParts.Skip(1).ToArray();

            foreach (var parameterPart in parameterParts)
            {
                var paramName = parameterPart.Split(' ')[0];
                var paramData = parameterPart.Replace(paramName, "").Trim(new char[] { ' ', '"', '=' });
                parameters.Add(paramName, paramData);
            }

            return parameters;
        }

        private bool HasRequiredParameters(Dictionary<string, Tuple<ParameterType, string>> paramDefinitions, Dictionary<string, string> parameters)
        {
            foreach (var paramDefinition in paramDefinitions)
            {
                if (paramDefinition.Value.Item1 == ParameterType.Required)
                {
                    if (!parameters.ContainsKey(paramDefinition.Key))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool HasUnknownParameters(Dictionary<string, Tuple<ParameterType, string>> paramDefinitions, Dictionary<string, string> parameters)
        {
            foreach (var parameter in parameters)
            {
                if (!paramDefinitions.ContainsKey(parameter.Key))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
