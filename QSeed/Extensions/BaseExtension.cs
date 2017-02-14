using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSeed.Extensions
{
    public static class BaseExtension
    {
        public static bool IsSubclassOfGenericInterface(this Type type, Type interfaceType)
        {
            return type.GetInterfaces()
                .Where(i => i.IsGenericType)
                .Any(i => i.GetGenericTypeDefinition() == interfaceType);
        }

        public static bool IsSubclassOfGenericClass(this Type type, Type interfaceType)
        {
            while (type != null && type != typeof(object))
            {
                var cur = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                if (interfaceType == cur)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
    }
}
