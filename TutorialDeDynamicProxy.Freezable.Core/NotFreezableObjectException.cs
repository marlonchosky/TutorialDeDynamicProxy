using System;
using System.Diagnostics.CodeAnalysis;

namespace TutorialDeDynamicProxy.FreezableApi.Core {
    [SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "No es necesario")]
    public class NotFreezableObjectException : Exception {
        public object Freezable { get; }

        public NotFreezableObjectException(object freezable) => this.Freezable = freezable;
    }
}