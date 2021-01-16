using Castle.DynamicProxy;
using Xunit;
using Xunit.Abstractions;

namespace TutorialDeDynamicProxy.MultipleInterfaces.Tests {
    public class UnitTest1 {
        private readonly ITestOutputHelper _helper;
        private readonly ProxyGenerator _generator;

        public UnitTest1(ITestOutputHelper helper) {
            this._helper = helper;
            this._generator = new ProxyGenerator();
        }

        [Fact]
        public void ClassProxy_should_implement_additional_interfaces() {
            var proxy = this._generator.CreateClassProxy(
                typeof(EnsurePartnerStatusRule),
                new[] { typeof(ISupportsInvalidation) },
                new InvalidationInterceptor());
            Assert.IsAssignableFrom<ISupportsInvalidation>(proxy);
        }

        [Fact]
        public void ClassProxy_for_class_already_implementing_additional_interfaces() {
            var proxy = this._generator.CreateClassProxy(
                typeof(ApplyDiscountRule),
                new[] { typeof(ISupportsInvalidation) });
            
            Assert.IsAssignableFrom<ISupportsInvalidation>(proxy);
            
            var exception = Record
                .Exception(() => ((ISupportsInvalidation)proxy).Invalidate());
            Assert.Null(exception);
        }
        
        [Fact]
        public void InterfaceProxy_should_implement_additional_interfaces() {
            var proxy = this._generator.CreateInterfaceProxyWithTarget(
                typeof(IClientRule),
                new[] {typeof(ISupportsInvalidation)},
                new ApplyDiscountRule());
            
            Assert.IsAssignableFrom<ISupportsInvalidation>(proxy);
            
            var exception = Record
                .Exception(() => ((ISupportsInvalidation)proxy).Invalidate());
            Assert.Null(exception);
        }
    }
}
