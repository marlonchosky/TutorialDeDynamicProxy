using System;

namespace TutorialDeDynamicProxy.FreezableApi.Core {
    public class WithNonVirtualMethod : Pet {
        public void NonVirtualMethod() => Console.WriteLine("Dentro de NonVirtualMethod");
    }
}