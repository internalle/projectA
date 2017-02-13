using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSeed
{
    public abstract class HasFactory
    {
        internal FactoryAndSeederFactory _factory { get; set; }

        internal void SetFactory(FactoryAndSeederFactory factory)
        {
            _factory = factory;
        }
    }
}
