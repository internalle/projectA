using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core.Features.Locations
{
    public class MeasurementStation : ActiveRecord<MeasurementStation>
    {
        public virtual string Name { get; set; }

        public virtual City City { get; set; }

        public virtual int ApiId { get; set; }

        public virtual decimal Latitiude { get; set; }

        public virtual decimal Longitude { get; set; }
    }
}
