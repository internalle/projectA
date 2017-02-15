using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMand.Executors.Impl
{
    public class ClsExecutor : Executor
    {
        public override string Description => "Clears the command line";

        public override string Name => "cls";

        public override void Execute(string line)
        {
            if (Marshal.Output == System.Console.WriteLine)
            {
                System.Console.Clear();
            }
        }
    }
}
