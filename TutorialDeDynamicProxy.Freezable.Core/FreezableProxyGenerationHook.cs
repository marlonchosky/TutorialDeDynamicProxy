using Castle.DynamicProxy;
using System;
using System.Reflection;

namespace TutorialDeDynamicProxy.FreezableApi.Core {
    public class FreezableProxyGenerationHook : IProxyGenerationHook {
        public void MethodsInspected() { }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo) {
            if (memberInfo is MethodInfo method)
                ValidateNotSetter(method);
        }

        private static void ValidateNotSetter(MethodBase method) {
            if (method.IsSpecialName && IsSetterName(method.Name))
                throw new InvalidOperationException($"Property {method.Name.Substring("set_".Length)} " +
                                                    "is not virtual. Can't freeze classes with non-virtual properties.");
        }

        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
            => methodInfo.IsSpecialName &&
               (IsSetterName(methodInfo.Name) || IsGetterName(methodInfo.Name));

        private static bool IsGetterName(string name) => name.StartsWith("get_", StringComparison.Ordinal);

        private static bool IsSetterName(string name) => name.StartsWith("set_", StringComparison.Ordinal);
    }
}
