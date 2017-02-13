using Qmand.Commands.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qmand.Commands
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract List<CommandParameter>  ParametersDefinition { get; }

        internal Dictionary<string, string> Parameters { get; set; }

        public string GetParametar(string name)
        {
            return Parameters.ContainsKey(name) ? Parameters[name] : null; 
        }

        public abstract void Run();
    }
}
