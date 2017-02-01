using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qmand
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract Dictionary<string, ParameterType> ParametersDefinition { get; }

        public Dictionary<string, string> Parameters { get; internal set; }

        public abstract void Run();
    }
}
