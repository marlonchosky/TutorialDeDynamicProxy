using Castle.DynamicProxy;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TutorialDeDynamicProxy.FreezableApi.Core;
using Xunit;

namespace TutorialDeDynamicProxy.FreezableApi.Tests {
    public class FreezableDeberia {
        [Fact]
        public void IsFreezable_should_be_false_for_objects_created_with_ctor() {
            var nonFreezablePet = new Pet();
            Assert.False(Freezable.IsFreezable(nonFreezablePet));
        }

        [Fact]
        public void IsFreezable_should_be_true_for_objects_created_with_MakeFreezable() {
            var freezablePet = Freezable.MakeFreezable<Pet>();
            Assert.True(Freezable.IsFreezable(freezablePet));
        }

        [Fact]
        public void Freezable_should_work_normally() {
            var pet = Freezable.MakeFreezable<Pet>();
            pet.Age = 3;
            pet.Deceased = true;
            pet.Name = "Rex";
            pet.Age += pet.Name.Length;
            Assert.NotNull(pet.ToString());
        }

        [Fact]
        public void Frozen_object_should_throw_ObjectFrozenException_when_trying_to_set_a_property() {
            var pet = Freezable.MakeFreezable<Pet>();
            pet.Age = 3;

            Freezable.Freeze(pet);
            Assert.Throws<ObjectFrozenException>(() => pet.Name = "This should throw");
        }

        [Fact]
        public void Frozen_object_should_not_throw_when_trying_to_read_it() {
            var pet = Freezable.MakeFreezable<Pet>();
            pet.Age = 3;

            Freezable.Freeze(pet);
            _ = pet.Age;
            _ = pet.Name;
            _ = pet.Deceased;
            _ = pet.ToString();
        }

        [Fact]
        public void Freeze_nonFreezable_object_should_throw_NotFreezableObjectException() {
            var rex = new Pet();
            Assert.Throws<NotFreezableObjectException>(() => Freezable.Freeze(rex));
        }

        [Fact]
        public void Freezable_should_not_intercept_property_getters() {
            var pet = Freezable.MakeFreezable<Pet>();
            Freezable.Freeze(pet);
            _ = pet.Age; //should not intercept
            var interceptedMethodsCount = GetInterceptedMethodsCountFor<FreezableInterceptor>(pet);
            Assert.Equal(0, interceptedMethodsCount);
        }

        private static int GetInterceptedMethodsCountFor<TInterceptor>(object @object) where TInterceptor : ICountable {
            Assert.True(Freezable.IsFreezable(@object));

            var hack = @object as IProxyTargetAccessor;
            if (hack is null) {
                Assert.NotNull(hack);
                throw new Exception();
            }

            var loggingInterceptor = hack.GetInterceptors().OfType<TInterceptor>().Single();
            return loggingInterceptor.Count;
        }

        [Fact]
        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Es necesario para Resharper")]
        public void DynProxyGetTarget_should_return_proxy_itself() {
            var pet = Freezable.MakeFreezable<Pet>();
            if (pet is not IProxyTargetAccessor hack) throw new Exception();

            Assert.NotNull(hack);
            Assert.Same(pet, hack.DynProxyGetTarget());
        }

        [Fact]
        [SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Necesario para la prueba")]
        [SuppressMessage("ReSharper", "RedundantAssignment")]
        [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Necesario para Resharper")]
        public void Freezable_should_not_hold_any_reference_to_created_objects() {
#if !DEBUG
            //var pet = TutorialDeDynamicProxy.FreezableApi.Core.Old.Freezable.MakeFreezable<Pet>();
            var pet = Freezable.MakeFreezable<Pet>();
            var petWeakReference = new WeakReference(pet, false);
            pet = null;

            GC.Collect();
            Assert.False(petWeakReference.IsAlive, "Object should have been collected");
#endif
        }

        [Fact]
        public void Freezable_should_log_getters_and_setters() {
            var pet = Freezable.MakeFreezable<Pet>();
            pet.Age = 4;
            _ = pet.Age;

            var logsCount = GetInterceptedMethodsCountFor<CallLoggingInterceptor>(pet);
            var freezeCount = GetInterceptedMethodsCountFor<FreezableInterceptor>(pet);

            Assert.Equal(2, logsCount);
            Assert.Equal(1, freezeCount);
        }

        [Fact]
        public void Freezable_should_not_intercept_methods() {
            var pet = Freezable.MakeFreezable<Pet>();
            _ = pet.ToString();

            var logsCount = GetInterceptedMethodsCountFor<CallLoggingInterceptor>(pet);
            var freezeCount = GetInterceptedMethodsCountFor<FreezableInterceptor>(pet);

            Assert.Equal(3, logsCount);
            Assert.Equal(0, freezeCount);
        }

        [Fact]
        public void Freezable_should_freeze_classes_with_nonVirtual_methods() {
            var pet = Freezable.MakeFreezable<WithNonVirtualMethod>();
            pet.Name = "Rex";
            pet.NonVirtualMethod();

            var logsCount = GetInterceptedMethodsCountFor<CallLoggingInterceptor>(pet);
            var freezeCount = GetInterceptedMethodsCountFor<FreezableInterceptor>(pet);

            Assert.Equal(1, logsCount);
            Assert.Equal(1, freezeCount);
        }

        [Fact]
        public void Freezable_should_throw_when_trying_to_freeze_classes_with_nonVirtual_setters() {
            var exception = Assert.Throws<InvalidOperationException>(() => Freezable.MakeFreezable<WithNonVirtualSetter>());
            const string nombreDePropiedadNoVirtual = nameof(WithNonVirtualSetter.NonVirtualProperty);
            Assert.Equal(
                $"Property {nombreDePropiedadNoVirtual} is not virtual. Can't freeze classes with non-virtual properties.",
                exception.Message);
        }

        [Fact]
        [Obsolete]
        public void NoCrearSinConstructorPorDefecto() => 
            Assert.Throws<InvalidProxyConstructorArgumentsException>(Core.Old.v2.Freezable.MakeFreezable<Mascota>);
    }
}
