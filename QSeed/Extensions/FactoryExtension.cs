using QSeed.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QSeed.Extensions
{
    public static class FactoryExtension
    {
        public static bool IsModelFactory(this Type type)
        {
            return type.IsSubclassOfGenericClass(typeof(ModelFactory<>));
        }

        public static bool IsModelFactory<T>(this Type type)
        {
            return type.IsSubclassOf(typeof(ModelFactory<T>));
        }

        public static IEnumerable<Type> GetModelFactoryTypes(this Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(x => x.IsModelFactory());
        }
    }
}
