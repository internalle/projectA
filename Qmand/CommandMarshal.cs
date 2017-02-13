using Qmand.Commands;
using Qmand.Executors;
using Qmand.Executors.Impl;
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
        internal Action<object> Output { get; set; }

        internal Dictionary<string, Type> Commands { get; set; }

        internal List<Type> Executors { get; set; }

        public CommandMarshal(Action<object> output = null)
        {
            Output = output ?? Console.WriteLine;
            Commands = new Dictionary<string, Type>();
            Executors = new List<Type>
            {
                typeof(RunExecutor),
                typeof(HelpExecutor)
            };
        }

        public void RegisterCommand(Type commandType)
        {
            var isCorrectType = commandType.IsSubclassOf(typeof(ConsoleCommand));

            if (!isCorrectType)
            {
                throw new InvalidCastException($"Expected command of type {typeof(ConsoleCommand)}");
            }

            var commandInstance = Activator.CreateInstance(commandType) as ConsoleCommand;
            
            Commands.Add(commandInstance.Name.ToLowerInvariant(), commandType);
        }

        public void RegisterCommandAssembly(Assembly assembly)
        {
            var commandTypes = assembly.GetExportedTypes().Where(x => x.IsSubclassOf(typeof(ConsoleCommand))).ToList();

            foreach (var commandType in commandTypes)
            {
                RegisterCommand(commandType);
            }
        }

        public void ExecuteCommandString(string command)
        {
            try
            {
                foreach (var executor in Executors)
                {
                    var instance = CreateExecutorInstance(executor);
                    if (instance.IsForThisExecutor(command))
                    {
                        instance.Execute(command);
                    }
                }
            }catch(Exception ex)
            {
                Output.Invoke(ex);
            }
        }

        private BaseExecutor CreateExecutorInstance(Type executorType)
        {
            var instance = Activator.CreateInstance(executorType) as BaseExecutor;
            instance.Commands = Commands;
            instance.SetOutput(Output);

            return instance;
        }
    }
}
