using System;
using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.Caching.Tests {
    public class BarInterceptor : IInterceptor {
        public void Intercept(IInvocation invocation) {
            Console.WriteLine("En BarInterceptor");
            invocation.Proceed();
        }
    }
}