using Castle.DynamicProxy;
using System;

namespace TutorialDeDynamicProxy.InterfaceProxyWithoutTarget.ConsoleApp {
    public class MethodInterceptor : IInterceptor {
        public Delegate? Delegate { get; }

        public MethodInterceptor(Delegate? @delegate) => this.Delegate = @delegate;

        public void Intercept(IInvocation invocation) {
            if (this.Delegate is null) return;
            
            var result = this.Delegate.DynamicInvoke(invocation.Arguments);
            invocation.ReturnValue = result;
        }
    }
}