using Castle.DynamicProxy;

namespace TutorialDeDynamicProxy.InterfaceProxyWithTarget.ConsoleApp {
    public class TimeFix {
        private readonly ProxyGenerator _generator = new();
        private readonly ProxyGenerationOptions _options = new() { Selector = new TimeFixSelector()};

        public ITimeHelper Fix(ITimeHelper item)
            => this._generator.CreateInterfaceProxyWithTarget(item, this._options);
    }
}