using System;

namespace TutorialDeDynamicProxy.FreezableApi.Core {
    public class ObjectFrozenException : Exception {
        public ObjectFrozenException(string message) : base(message) { }

        public ObjectFrozenException(string message, Exception innerException) : base(message, innerException) { }

        public ObjectFrozenException() { }
    }
}
