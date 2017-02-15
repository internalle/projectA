using QMand.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMand.Executors.Impl
{
    public class MacroExecutor : Executor
    {
        public override string Description => "Executes a specified macro";

        public override string Name => "macro";

        public Dictionary<string, List<string>> Macros { get; set; } = new Dictionary<string, List<string>>
        {
            { "test", new List<string> { "help", "help <command>" } }
        };

        public override void Execute(string line)
        {
            var macro = line.GetRest().GetFirst();

            foreach(var command in Macros[macro])
            {
                var replacedCommand = command;
                foreach(var param in Parameters)
                {
                    replacedCommand = replacedCommand.Replace($"<{param.Key}>", param.Value);
                }
                Marshal.Output(replacedCommand);
                Marshal.ExecuteCommandString(replacedCommand);
                Marshal.Output("===================================================");
            }
        }
    }
}
