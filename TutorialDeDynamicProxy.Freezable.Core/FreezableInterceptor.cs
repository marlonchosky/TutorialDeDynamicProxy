using Castle.DynamicProxy;
using System;
using System.Reflection;

namespace TutorialDeDynamicProxy.FreezableApi.Core {
    public class FreezableInterceptor : IInterceptor, IFreezable, ICountable {
        public void Intercept(IInvocation invocation) {
            if (this.IsFrozen && IsSetter(invocation.Method))
                throw new ObjectFrozenException();

            invocation.Proceed();
            this.Count++;
        }

        private static bool IsSetter(MethodBase method) 
            => method.IsSpecialName && method.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase);

        public bool IsFrozen { get; private set; }

        public void Freeze() => this.IsFrozen = true;
        
        public int Count { get; private set; }
    }
}