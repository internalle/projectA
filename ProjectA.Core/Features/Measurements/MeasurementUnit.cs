using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core.Features.Measurements
{
    public class MeasurementUnit : ActiveRecord<MeasurementUnit>
    {
        public virtual string Name { get; set; }

        public virtual string Unit { get; set; }
    }
}
