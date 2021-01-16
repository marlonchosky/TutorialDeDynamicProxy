using System;
using System.Globalization;
using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.InterfaceProxyWithTarget.ConsoleApp {
    public class AdjustTimeToUtcInterceptor : IInterceptor {
        public void Intercept(IInvocation invocation) {
            var argument = (string) invocation.Arguments[0];
            if (DateTimeOffset.TryParse(argument, out var result)) {
                argument = result.UtcDateTime.ToString(CultureInfo.InvariantCulture);
                invocation.Arguments[0] = argument;
            }

            try {
                invocation.Proceed();
            } catch (FormatException) {
                invocation.ReturnValue = 0;
            }
        }
    }
}