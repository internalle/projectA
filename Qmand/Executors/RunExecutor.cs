using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qmand.Executors
{
    public class RunExecutor : BaseExecutor
    {
        protected override string ExecutorName => "run";

        protected override bool ShouldHaveCommand => true;

        public override void Execute(string line)
        {
            GetConsoleCommand(line).Run();
        }
    }
}
