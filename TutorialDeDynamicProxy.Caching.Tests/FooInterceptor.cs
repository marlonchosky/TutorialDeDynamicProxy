using System;
using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.Caching.Tests {
    public class FooInterceptor : IInterceptor {
        public void Intercept(IInvocation invocation) {
            Console.WriteLine("En FooInterceptor");
            invocation.Proceed();
        }
    }
}