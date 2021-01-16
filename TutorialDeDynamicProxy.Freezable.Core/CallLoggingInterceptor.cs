using System;
using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.FreezableApi.Core {
    public class CallLoggingInterceptor : IInterceptor, ICountable {
        private const ConsoleColor ColorDelLogging = ConsoleColor.Red;
        
        public int Count { get; set; }

        public void Intercept(IInvocation invocation) {
            var colorAnterior = Console.ForegroundColor;
            Console.ForegroundColor = ColorDelLogging;
            Console.WriteLine($">>>Interceptando: {invocation.Method.Name}", ColorDelLogging);
            Console.ForegroundColor = colorAnterior;
            
            invocation.Proceed();
            this.Count++;
        }
    }
}