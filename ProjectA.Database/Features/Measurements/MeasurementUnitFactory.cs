using ProjectA.Core.Features.Measurements;
using QSeed;
using QSeed.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using QSeed.Config;

namespace ProjectA.Seeders.Features.Measurements
{
    public class MeasurementUnitFactory : ModelFactory<MeasurementUnit>
    {
        protected override MeasurementUnit Build()
        {
            return new MeasurementUnit
            {
                Name = Faker.Lorem.Sentence(),
                Unit = Faker.Lorem.Characters(1),
            };
        }
    }
}
