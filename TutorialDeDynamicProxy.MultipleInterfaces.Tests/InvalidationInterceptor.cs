using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.MultipleInterfaces.Tests {
    public class InvalidationInterceptor : IInterceptor {
        public void Intercept(IInvocation invocation) {
            invocation.Proceed();
        }
    }
}