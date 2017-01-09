using ProjectA.Core.Features.Measurements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Seeders.Features.Measurements
{
    public class MeasuremetUnitSeeder : BaseSeeder<MeasurementUnit>
    {
        public MeasuremetUnitSeeder()
        {            
            Builder
                .UpdateOrAdd(new MeasurementUnit { Name = "PM25", Unit = "" }, x => x.Name == "PM25")
                .UpdateOrAdd(new MeasurementUnit { Name = "PM10", Unit = "" }, x => x.Name == "PM10")
                .UpdateOrAdd(new MeasurementUnit { Name = "O3", Unit = "" }, x => x.Name == "O3")
                .UpdateOrAdd(new MeasurementUnit { Name = "CO", Unit = "" }, x => x.Name == "CO")
                .UpdateOrAdd(new MeasurementUnit { Name = "NO2", Unit = "" }, x => x.Name == "NO2")
                .UpdateOrAdd(new MeasurementUnit { Name = "Temperatire", Unit = "" }, x => x.Name == "Temperatire")
                .UpdateOrAdd(new MeasurementUnit { Name = "Pressure", Unit = "" }, x => x.Name == "Pressure")
                .UpdateOrAdd(new MeasurementUnit { Name = "Humidity", Unit = "" }, x => x.Name == "Humidity");
        }
    }
}
