using System;
using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.InterfaceProxyWithoutTarget.ConsoleApp {
    public class DelegateWrapper {
        public static T WrapAs<T>(Delegate delegate1, Delegate? delegate2) where T : class {
            var generator = new ProxyGenerator();
            var options = new ProxyGenerationOptions {
                Selector = new DelegateSelector()
            };
            
            var proxy = generator.CreateInterfaceProxyWithoutTarget<T>(options, 
                new MethodInterceptor(delegate1),
                new MethodInterceptor(delegate2)
            );
            return proxy;
        }
    }
}
