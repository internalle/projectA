using QSeed.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSeed.ReflectionExtensions
{
    public static class RepositoryExtension
    {
        public static bool IsRepository(this Type type)
        {
            return type.IsSubclassOfGenericInterface(typeof(IRepository<>));
        }
    }
}
