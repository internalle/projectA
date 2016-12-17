using ProjectA.Core.Features.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core.Features.Measurements
{
    public class MeasurementValue : ActiveRecord<MeasurementValue>
    {
        public virtual MeasurementUnit Unit { get; set; }

        public virtual decimal Value { get; set; }

        public virtual MeasurementStation Station { get; set; }

        public virtual DateTime Timestamp { get; set; }
    }
}
