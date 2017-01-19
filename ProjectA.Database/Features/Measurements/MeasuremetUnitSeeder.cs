using ProjectA.Core.Features.Measurements;
using QSeed;
using QSeed.SeederTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QSeed.Config;

namespace ProjectA.Seeders.Features.Measurements
{
    public class MeasuremetUnitSeeder : BaseSeeder
    {
        public override void Run()
        {
            var muRepo = GetRepository<MeasurementUnit>();
            var muFactory = GetModelFactory<MeasurementUnit>();

            muRepo.ClearData();
            muRepo.Save(new MeasurementUnit { Name = "PM25", Unit = "" });
            muRepo.Save(new MeasurementUnit { Name = "PM10", Unit = "" });
            muRepo.Save(new MeasurementUnit { Name = "O3", Unit = "" });
            muRepo.Save(new MeasurementUnit { Name = "CO", Unit = "" });
            muRepo.Save(new MeasurementUnit { Name = "NO2", Unit = "" });
            muRepo.Save(new MeasurementUnit { Name = "Temperature", Unit = "" });
            muRepo.Save(new MeasurementUnit { Name = "Pressure", Unit = "" });
            muRepo.Save(new MeasurementUnit { Name = "Humidity", Unit = "" });
        }
    }
}
