using Castle.DynamicProxy;
using System;
using System.IO;
using Castle.Core.Logging;
using Xunit;
using Xunit.Abstractions;

namespace TutorialDeDynamicProxy.Caching.Tests {
    public class UnitTest1 {
        private readonly ITestOutputHelper _helper;

        public UnitTest1(ITestOutputHelper helper) => this._helper = helper;

        [Fact]
        public void Test1() {
            Console.SetOut(new Converter(this._helper));
            var generator = new ProxyGenerator {
                Logger = new StreamLogger("Test", new FileStream("Test.log", FileMode.OpenOrCreate))
            };

            var proxy1 = generator.CreateClassProxy<Foo>(new FooInterceptor());
            var proxy2 = generator.CreateClassProxy<Foo>(new BarInterceptor(), new FooInterceptor());
            Assert.Equal(proxy1.GetType(), proxy2.GetType());

            proxy1.Test();
        }
        
        [Fact]
        public void Test2() {
            Console.SetOut(new Converter(this._helper));
            var generator1 = new ProxyGenerator {
                Logger = new StreamLogger("Test", new FileStream("Test2.log", FileMode.OpenOrCreate))
            };
            var generator2 = new ProxyGenerator {
                Logger = new StreamLogger("Test", new FileStream("Test3.log", FileMode.OpenOrCreate))
            };

            var proxy1 = generator1.CreateClassProxy<Foo>(new FooInterceptor());
            var proxy2 = generator2.CreateClassProxy<Foo>(new BarInterceptor(), new FooInterceptor());
            Assert.Equal(proxy1.GetType(), proxy2.GetType());

            proxy1.Test();
        }
    }
}
