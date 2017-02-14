using QMand.Commands.Definition;
using QMand.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMand.Commands
{
    public abstract class ConsoleCommand
    {
        private Dictionary<string, string> Parameters { get; set; }

        internal void SetParameters(Dictionary<string, string> parameters)
        {
            SetParameters(parameters.ToList());
        }

        internal void SetParameters(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            Parameters = new Dictionary<string, string>();
            Parameters.Add(parameters.Where(x => ParametersDefinition.Any(y => y.Name == x.Key)));
        }

        public IEnumerable<string> UndefinedRequiredParameters()
        {
            return ParametersDefinition
                .Where(x => !Parameters.Any(y => y.Key == x.Name) && x.Type == ParameterType.Required)
                .Select(x=>x.Name);
        }

        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract List<CommandParameter> ParametersDefinition { get; }

        public abstract void Run();

        protected string GetParametar(string name)
        {
            return Parameters.ContainsKey(name) ? Parameters[name] : null;
        }
    }
}
