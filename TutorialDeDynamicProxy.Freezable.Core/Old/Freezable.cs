using System;
using System.Collections.Generic;
using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.FreezableApi.Core.Old {
    [Obsolete]
    public class Freezable {
        private static readonly IDictionary<object, IFreezable> InstanceMap = new Dictionary<object, IFreezable>();
        private static readonly ProxyGenerator Generator = new();
        
        public static bool IsFreezable(object @object) => InstanceMap.ContainsKey(@object);

        public static TFreezable MakeFreezable<TFreezable>() where TFreezable : class {
            var freezableInterceptor = new FreezableInterceptor();
            var options = new ProxyGenerationOptions(new FreezableProxyGenerationHook());
            
            var proxy = Generator.CreateClassProxy<TFreezable>(options, new CallLoggingInterceptor(), freezableInterceptor);
            InstanceMap.Add(proxy, freezableInterceptor);

            return proxy;
        }

        public static void Freeze(object freezable) {
            if (!IsFreezable(freezable))
                throw new NotFreezableObjectException(freezable);
            InstanceMap[freezable].Freeze();
        }

        public static bool IsFrozen(object freezable) => IsFreezable(freezable) && InstanceMap[freezable].IsFrozen;
    }
}
