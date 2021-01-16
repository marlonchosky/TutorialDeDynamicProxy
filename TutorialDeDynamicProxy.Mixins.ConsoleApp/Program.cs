using Castle.DynamicProxy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TutorialDeDynamicProxy.Mixins.ConsoleApp {
    internal class Program {
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Necesario para Resharper")]
        private static void Main() {
            var person = CreateProxy();
            var dictionary = (IDictionary)person;
            dictionary.Add("Next Leave", DateTime.Now.AddMonths(4));
            UseSomewhereElse(person);
        }

        private static Person CreateProxy() {
            var generator = new ProxyGenerator();
            var options = new ProxyGenerationOptions();
            options.AddMixinInstance(new Dictionary<string, object>());

            return generator.CreateClassProxy<Person>(options);
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Necesario para Resharper")]
        private static void UseSomewhereElse(Person person) {
            var dictionary = (IDictionary<string, object>)person;
            var date = ((DateTime)dictionary["Next Leave"]).Date;
            Console.WriteLine($"Next leave date of {person.Name} is {date}");
        }
    }
}
