using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qmand
{
    public class CommandMarshal
    {
        public static Dictionary<string, ConsoleCommand> Commands { get; set; } = new Dictionary<string, ConsoleCommand>();

        public static void RegisterCommand(Type command)
        {
            var isCorrectType = command.IsSubclassOf(typeof(ConsoleCommand));

            if (!isCorrectType)
            {
                throw new InvalidCastException($"Expected command of type {typeof(ConsoleCommand)}");
            }

            var instance = Activator.CreateInstance(command) as ConsoleCommand;
            Commands.Add(instance.Name.ToLowerInvariant(), instance);
        }

        public static void RegisterCommandAssembly(Assembly assembly)
        {
            var commandTypes = assembly.GetExportedTypes().Where(x => x.IsSubclassOf(typeof(ConsoleCommand))).ToList();

            foreach (var commandType in commandTypes)
            {
                RegisterCommand(commandType);
            }
        }

        public static void ExecuteCommandString(string command)
        {
            if (IsRunnerCommand(command))
            {
                var commandName = GetCommandName(command);
                var commandParams = GetCommandParameters(command);

                if (!Commands.ContainsKey(commandName))
                {
                    throw new Exception("Unknown command");
                }

                var instance = Commands[commandName];

                if (!HasRequiredParameters(instance.ParametersDefinition, commandParams))
                {
                    throw new Exception("Required parameters are not defined");
                }

                if (!HasUnknownParameters(instance.ParametersDefinition, commandParams))
                {
                    throw new Exception("Some parameters are not defined");
                }

                instance.Parameters = commandParams;
                instance.Run();
            }
        }

        private static bool IsRunnerCommand(string command)
        {
            return command.StartsWith("run ");
        }

        private static string GetCommandName(string command)
        {
            return command.Replace("run ", "").Split(' ')[0];
        }

        private static Dictionary<string, string> GetCommandParameters(string command)
        {
            var parameters = new Dictionary<string, string>();

            var parameterParts = command.Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
            parameterParts = parameterParts.Skip(1).ToArray();

            foreach (var parameterPart in parameterParts)
            {
                var paramName = parameterPart.Split(' ')[0];
                var paramData = parameterPart.Replace(paramName, "").Trim(new char[] { ' ', '"' });
                parameters.Add(paramName, paramData);
            }

            return parameters;
        }

        private static bool HasRequiredParameters(Dictionary<string, ParameterType> paramDefinitions, Dictionary<string, string> parameters)
        {
            foreach (var paramDefinition in paramDefinitions)
            {
                if (paramDefinition.Value == ParameterType.Required)
                {
                    if (!parameters.ContainsKey(paramDefinition.Key))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool HasUnknownParameters(Dictionary<string, ParameterType> paramDefinitions, Dictionary<string, string> parameters)
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
