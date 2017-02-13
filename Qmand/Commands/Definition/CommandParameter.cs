using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qmand.Commands.Definition
{
    public class CommandParameter
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ParameterType Type { get; set; }
    }
}