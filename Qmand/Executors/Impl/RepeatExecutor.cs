using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QMand.Executors.Impl
{
    public class RepeatExecutor : Executor
    {
        public override string Description =>
@"Runs a specific command several times with a timeout

repeat <CommandName> [--<Param1Name> <Param1Value>, ...] --times <Value>, --timeout <MillisecondsValue>";

        public override string Name => "repeat";

        public override void Execute(string line)
        {
            var command = GetConsoleCommand(line);

            foreach(var urp in command.UndefinedRequiredParameters())
            {
                throw new ArgumentNullException(urp);
            }

            var times = int.Parse(GetParametar("times", "1"));
            var timeout = Math.Max(int.Parse(GetParametar("timeout", "0")), 0);

            for (int i = 0; i < times || times < 0; i++)
            {
                Thread.Sleep(timeout);
                command.Run();
            }
        }
    }
}
