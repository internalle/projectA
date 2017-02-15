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
        protected Dictionary<string, string> Parameters{ get; set; }

        public Executor()
        {
            Parameters = new Dictionary<string, string>();
        }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract void Execute(string line);

        public CommandMarshal Marshal { get; set; }

        internal bool IsForThisExecutor(string line)
        {
            return line.GetFirst() == Name;
        }

        protected string GetParameter(string name, string defaultValue = null)
        {
            return Parameters.ContainsKey(name) ? Parameters[name] : defaultValue;
        }

        internal void SetParameters(string line)
        {
            Parameters.Add(line.GetCommandParameters());
        }

        protected virtual ConsoleCommand GetConsoleCommand(string line)
        {
            var commandName = line.GetRest().GetFirst();
            var lineParams = line.GetCommandParameters();

            if (!Marshal.Commands.ContainsKey(commandName))
            {
                throw new Exception("Unknown command");
            }

            var commandInstance = Activator.CreateInstance(Marshal.Commands[commandName]) as ConsoleCommand;

            var commandParams = lineParams.Where(x => commandInstance.ParametersDefinition.Any(y => y.Name == x.Key));
            var executorParams = lineParams.Where(x => commandInstance.ParametersDefinition.Any(y => y.Name != x.Key));

            commandInstance.SetParameters(commandParams);

            return commandInstance;
        }
    }
}
