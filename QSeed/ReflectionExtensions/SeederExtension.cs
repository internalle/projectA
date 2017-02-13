using QSeed.SeederTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QSeed.ReflectionExtensions
{
    public static class SeederExtension
    {

        public static bool IsBaseSeeder(this Type type)
        {
            return type.IsSubclassOf(typeof(BaseSeeder));
        }

        public static IEnumerable<Type> GetBaseSeederTypes(this Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(x => x.IsBaseSeeder());
        }



        public static bool IsMasterSeeder(this Type type)
        {
            return type.IsSubclassOf(typeof(MasterSeeder));
        }

        public static IEnumerable<Type> GetMasterSeederTypes(this Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(x => x.IsMasterSeeder());
        }
    }
}
