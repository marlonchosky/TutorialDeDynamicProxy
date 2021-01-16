using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.InterfaceProxyWithTarget.ConsoleApp {
    public class CheckNullInterceptor : IInterceptor {
        public void Intercept(IInvocation invocation) {
            if (invocation.Arguments[0] is null) {
                invocation.ReturnValue = 0;
                return;
            }
            
            invocation.Proceed();
        }
    }
}