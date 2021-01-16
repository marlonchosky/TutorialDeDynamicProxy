using System;
using System.Linq;
using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.FreezableApi.Core.Old.v2 {
    [Obsolete]
    public class Freezable {
        private static readonly ProxyGenerator Generator = new();
        private static readonly IInterceptorSelector Selector = new FreezableInterceptorSelector();

        public static TFreezable MakeFreezable<TFreezable>() where TFreezable : class {
            var freezableInterceptor = new FreezableInterceptor();
            var options = new ProxyGenerationOptions(new FreezableProxyGenerationHook()) {
                Selector = Selector
            };

            var proxy = Generator.CreateClassProxy<TFreezable>(options, new CallLoggingInterceptor(), freezableInterceptor);
            return proxy;
        }

        public static bool IsFreezable(object @object) => AsFreezable(@object) is { };
        
        public static void Freeze(object freezable) {
            var interceptor = AsFreezable(freezable);
            if (interceptor is null)
                throw new NotFreezableObjectException(freezable);
            interceptor.Freeze();
        }

        public static bool IsFrozen(object @object) {
            var freezable = AsFreezable(@object);
            return freezable is { } && freezable.IsFrozen;
        }
        
        private static IFreezable? AsFreezable(object target)
            => target is not IProxyTargetAccessor hack
                ? null : hack.GetInterceptors().OfType<FreezableInterceptor>().FirstOrDefault();
    }
}
