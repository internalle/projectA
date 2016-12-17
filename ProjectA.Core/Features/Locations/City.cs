using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Core.Features.Locations
{
    public class City : ActiveRecord<City>
    {
        public virtual string Name { get; set; }
    }
}
