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
        
        public static void Init()
        {
            var units = new List<MeasurementUnit>
            {
                new MeasurementUnit { Name = "pm25", Unit = "" },
                new MeasurementUnit { Name = "pm10", Unit = "" },
                new MeasurementUnit { Name = "o3", Unit = "" },
                new MeasurementUnit { Name = "co", Unit = "" },
                new MeasurementUnit { Name = "no2", Unit = "" },
                new MeasurementUnit { Name = "Temperatire", Unit = "" },
                new MeasurementUnit { Name = "Preasure", Unit = "" },
                new MeasurementUnit { Name = "Humidity", Unit = "" }
            };

            foreach (var unit in units)
            {
                var existing = Query(x => x.Name == unit.Name).SingleOrDefault();
                if (existing == null)
                {
                    unit.Save();
                }
            }
        }
    }
}
