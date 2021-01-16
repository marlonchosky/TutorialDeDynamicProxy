using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace TutorialDeDynamicProxy.FreezableApi.Core {
    public class FreezableInterceptorSelector : IInterceptorSelector {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors) 
            => IsSetter(method) ? interceptors : interceptors.Where(i => i is not FreezableInterceptor).ToArray();

        private static bool IsSetter(MethodBase method)
            => method.IsSpecialName && method.Name.StartsWith("set_", StringComparison.Ordinal);
    }
}