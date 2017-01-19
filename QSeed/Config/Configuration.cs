using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSeed.Config
{
    internal class Configuration
    {
        public Type Repository { get; set; }

        public Type MasterSeeder { get; set; }

        public IEnumerable<Type> BaseSeeders { get; set; }

        public IEnumerable<Type> ModalFactories { get; set; }
    }
}
