using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSeed
{
    public abstract class HasActuator
    {
        protected Actuator _actuator { get; set; }

        internal void SetActuator(Actuator actuator)
        {
            _actuator = actuator;
        }
    }
}
