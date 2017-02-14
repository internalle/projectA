using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMand.Executors.Impl
{
    public class RunExecutor : Executor
    {
        public override string Description =>
@"Runs a specific command

run <CommandName> [--<Param1Name> <Param1Value>, ...]";

        public override string Name => "run";

        public override void Execute(string line)
        {
            var command = GetConsoleCommand(line);

            foreach(var urp in command.UndefinedRequiredParameters())
            {
                throw new ArgumentNullException(urp);
            }

            command.Run();
        }
    }
}
