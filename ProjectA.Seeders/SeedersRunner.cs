using ProjectA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Seeders
{
    public class SeedersRunner
    {
        private List<Type> _seeders { get; set; }

        public SeedersRunner()
        {
            _seeders = new List<Type>();
        }

        public SeedersRunner RegisterAssembly(Assembly assembly)
        {
            var scannedSeeders = assembly.GetExportedTypes().Where(x => x.BaseType.Name == typeof(BaseSeeder<>).Name);
            _seeders = _seeders.Concat(scannedSeeders).ToList();

            return this;
        }

        public SeedersRunner RegisterSeeder(Type seeder)
        {
            if (seeder.BaseType.Name != typeof(BaseSeeder<>).Name)
            {
                throw new InvalidCastException();
            }

            _seeders.Add(seeder);
            return this;
        }

        public void Run()
        {
            _seeders = _seeders.Distinct().ToList();
            foreach (var seeder in _seeders)
            {
                Activator.CreateInstance(seeder);
            }
        }
    }
}
