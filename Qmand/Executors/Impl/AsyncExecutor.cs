using QMand.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMand.Executors.Impl
{
    public class AsyncExecutor : Executor
    {
        public override string Description =>
@"Execute command asyncroniously

async <ExecutorName> [<CommandName>] [--Param1 <Param1Value> ...]";

        public override string Name => "async";

        public override void Execute(string line)
        {
            line = line.GetRest();
            run(line);
        }

        private async void run(string line)
        {
            await Task.Run(() =>
            {
                Marshal.ExecuteCommandString(line);
            });
        }
    }
}
