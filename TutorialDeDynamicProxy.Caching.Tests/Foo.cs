using System;

namespace TutorialDeDynamicProxy.Caching.Tests {
    public class Foo {
        public virtual int Test() => new Random().Next(10);
    }
}