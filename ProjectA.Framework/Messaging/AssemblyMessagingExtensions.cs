using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectA.Framework.Messaging
{
    public static class AssemblyMessagingExtensions
    {
        public static Type[] GetHandlerTypes(this System.Reflection.Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(x => x.Name != "BaseHandler" && x.Name.EndsWith("Handler")).ToArray();
        }
        public static Type[] GetValidatorTypes(this System.Reflection.Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(x => x.Name != "BaseValidator" && x.Name.EndsWith("Validator")).ToArray();
        }
        public static Type[] GetRequestTypes(this System.Reflection.Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(x => x.Name != "BaseRequest" && x.Name.EndsWith("Request")).ToArray();
        }
    }
}
