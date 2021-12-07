using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.UserManagement.Helpers
{
    public static class ReflectionHelpers
    {
        public static List<Type> GetTypeOfImplementedClassesFromInterface
            (Type interfaceType, Type onOfImplementedClassType)
        {

            var types = System.Reflection.Assembly.GetAssembly(onOfImplementedClassType)
            .GetTypes()
            .Where(item => item.GetInterfaces()
            .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == interfaceType)
                    && !item.IsAbstract && !item.IsInterface)
            .ToList();
            return types;
        }
    }
}
