using QMand.Commands;
using QMand.Commands.Definition;
using QMand.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMand.Executors
{
    public abstract class Executor
    {
        private Dictionary<string, string> Parameters { get; set; }

        public Executor()
        {
            Parameters = new Dictionary<string, string>();
        }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract void Execute(string command);

        internal Action<object> Output { get; set; }

        internal Dictionary<string, Type> Commands { get; set; }

        internal Dictionary<string, Type> Executors { get; set; }

        internal bool IsForThisExecutor(string line)
        {
            return line.GetExecutorName() == Name;
        }
        
        protected string GetParametar(string name, string defaultValue = null)
        {
            return Parameters.ContainsKey(name) ? Parameters[name] : defaultValue;
        }

        protected virtual ConsoleCommand GetConsoleCommand(string line)
        {
            var commandName = line.GetCommandName();
            var lineParams = line.GetCommandParameters();

            if (!Commands.ContainsKey(commandName))
            {
                throw new Exception("Unknown command");
            }

            var commandInstance = Activator.CreateInstance(Commands[commandName]) as ConsoleCommand;

            var commandParams = lineParams.Where(x => commandInstance.ParametersDefinition.Any(y => y.Name == x.Key));
            var executorParams = lineParams.Where(x => commandInstance.ParametersDefinition.Any(y => y.Name != x.Key));

            commandInstance.SetParameters(commandParams);
            Parameters.Add(executorParams);

            return commandInstance;
        }

        protected virtual Executor GetExecutor(string line)
        {
            var executorName = line.GetCommandName();

            if (!Executors.ContainsKey(executorName))
            {
                throw new Exception("Unknown executor");
            }

            var commandInstance = Activator.CreateInstance(Executors[executorName]) as Executor;            
            return commandInstance;
        }
    }
}
