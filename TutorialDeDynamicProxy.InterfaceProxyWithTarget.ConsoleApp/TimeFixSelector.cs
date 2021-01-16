using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.InterfaceProxyWithTarget.ConsoleApp {
    public class TimeFixSelector : IInterceptorSelector {
        private static readonly MethodInfo[] methodsToAdjust = {
            typeof(ITimeHelper).GetMethod(nameof(ITimeHelper.GetHour))!,
            typeof(ITimeHelper).GetMethod(nameof(ITimeHelper.GetMinute))!
        };

        private readonly CheckNullInterceptor _checkNull = new();
        private readonly AdjustTimeToUtcInterceptor _utcAdjust = new();

        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
            => !methodsToAdjust.Contains(method)
                ? new IInterceptor[] { this._checkNull }.Union(interceptors).ToArray()
                : new IInterceptor[] { this._checkNull, this._utcAdjust }.Union(interceptors).ToArray();
    }
}