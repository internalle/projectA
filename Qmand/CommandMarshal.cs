using Qmand.Executors;
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
        internal static Dictionary<string, Type> Commands { get; set; } = new Dictionary<string, Type>();

        internal static List<BaseExecutor> Executors => new List<BaseExecutor> { new HelpExecutor(), new RunExecutor() };

        public static void RegisterCommand(Type commandType)
        {
            var isCorrectType = commandType.IsSubclassOf(typeof(ConsoleCommand));

            if (!isCorrectType)
            {
                throw new InvalidCastException($"Expected command of type {typeof(ConsoleCommand)}");
            }

            var commandInstance = Activator.CreateInstance(commandType) as ConsoleCommand;
            
            Commands.Add(commandInstance.Name.ToLowerInvariant(), commandType);
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
            try
            {
                command = command.ToLowerInvariant();
                foreach (var executor in Executors)
                {
                    if (executor.IsForThisExecutor(command))
                    {
                        executor.Execute(command);
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
