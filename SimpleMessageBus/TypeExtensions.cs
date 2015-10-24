using System;
using System.Linq;

namespace SimpleMessageBus {
    //taken from https://bradhe.wordpress.com/2010/07/27/how-to-tell-if-a-type-implements-an-interface-in-net/
    //and also from http://stackoverflow.com/a/5976618
    public static class TypeExtensions {
        public static bool IsImplementationOf<TInterface>(this Type baseType) where TInterface : class {
            var interfaceType = typeof(TInterface);

            if (!interfaceType.IsInterface) { throw new InvalidOperationException("Only interfaces can be implemented."); }

            return baseType.GetInterfaces().Any(interfaceType.Equals);
        }
    }
}