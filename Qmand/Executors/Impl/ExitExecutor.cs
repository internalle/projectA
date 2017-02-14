using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMand.Executors.Impl
{
    public class ExitExecutor : Executor
    {
        public override string Description => "Exits the application";

        public override string Name => "exit";

        public override void Execute(string line)
        {
            Environment.Exit(0);
        }
    }
}
