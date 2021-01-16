using System;
using System.Diagnostics.CodeAnalysis;
using TutorialDeDynamicProxy.FreezableApi.Core;

namespace TutorialDeDynamicProxy.FreezableApi.ConsoleApp {
    internal class Program {
        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Se trata de una excepcion específica.")]
        private static void Main() {
            var rex = Freezable.MakeFreezable<Pet>();
            rex.Name = "Rex";
            Console.WriteLine(Freezable.IsFreezable(rex)
                ? "Rex is freezable"
                : "Rex is not freezable. Something is not working");
            Console.WriteLine(rex.ToString());
            
            Console.WriteLine("Add 50 years");
            rex.Age += 50;
            Console.WriteLine($"Age: {rex.Age}");

            rex.Deceased = true;
            Console.WriteLine($"Deceased: {rex.Deceased}");
            Freezable.Freeze(rex);

            try {
                rex.Age++;
            } catch (ObjectFrozenException) {
                Console.WriteLine("Oops. it's frozen. Can't change that anymore");
            }
            
            Console.WriteLine("--- press enter to close");
            Console.ReadLine();
        }
    }
}
