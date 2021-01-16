using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.InterfaceProxyWithoutTarget.ConsoleApp {
    public class DelegateSelector : IInterceptorSelector {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors) {
            foreach (var interceptor in interceptors) {
                if (interceptor is not MethodInterceptor methodInterceptor) continue;

                var @delegate = methodInterceptor.Delegate;
                if (IsEquivalent(@delegate, method))
                    return new[] {interceptor};
            }

            throw new ArgumentException();
        }

        [SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "No es necesario aplicar por claridad del codigo")]
        private static bool IsEquivalent(Delegate? @delegate, MethodInfo method) {
            var dm = @delegate.Method;
            if (!method.ReturnType.IsAssignableFrom(dm.ReturnType))
                return false;

            var parameters = method.GetParameters();
            var dp = dm.GetParameters();
            if (parameters.Length != dp.Length)
                return false;

            return !parameters.Where((t, i) => !t.ParameterType.IsAssignableFrom(dp[i].ParameterType)).Any();
        }
    }
}