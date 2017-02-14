using QMand.Commands;
using QMand.Executors;
using QMand.Executors.Impl;
using QMand.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QMand
{
    public class CommandMarshal
    {
        internal Action<object> Output { get; set; }

        internal Dictionary<string, Type> Commands { get; set; }

        internal Dictionary<string, Type> Executors { get; set; }

        public CommandMarshal(Action<object> output = null)
        {
            Output = output ?? Console.WriteLine;
            Commands = new Dictionary<string, Type>();
            Executors = new Dictionary<string, Type>();

            Executors.Add(typeof(Executor)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.IsSubclassOf(typeof(Executor)))
                .Select(x=> new KeyValuePair<string, Type>((Activator.CreateInstance(x) as Executor).Name, x)));
        }

        public void RegisterExecutor(Type type)
        {
            Register(Executors, type, typeof(Executor));
        }

        public void RegisterExecutorAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetExportedTypes())
            {
                try
                {
                    RegisterExecutor(type);
                }
                catch { }
            }
        }

        public void RegisterCommand(Type type)
        {
            Register(Commands, type, typeof(ConsoleCommand));
        }

        public void RegisterCommandAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetExportedTypes())
            {
                try
                {
                    RegisterCommand(type);
                }
                catch { }
            }
        }

        private void Register(Dictionary<string, Type> haystack, Type toAdd, Type expectedType)
        {
            var isCorrectType = toAdd.IsSubclassOf(expectedType);

            if (!isCorrectType)
            {
                throw new InvalidCastException($"Expected type {expectedType}");
            }

            var instance = Activator.CreateInstance(toAdd) as dynamic;

            haystack.Add(instance.Name.ToLowerInvariant(), toAdd);
        }

        public void ExecuteCommandString(string command)
        {
            try
            {
                foreach (var executor in Executors)
                {
                    var instance = CreateExecutorInstance(executor.Value);
                    if (instance.IsForThisExecutor(command))
                    {
                        instance.Execute(command);
                    }
                }
            }
            catch (Exception ex)
            {
                Output.Invoke(ex);
            }
        }

        private Executor CreateExecutorInstance(Type executorType)
        {
            var instance = Activator.CreateInstance(executorType) as Executor;
            instance.Commands = Commands;
            instance.Executors = Executors;
            instance.Output = Output;

            return instance;
        }
    }
}
