using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TutorialDeDynamicProxy.InterfaceProxyWithoutTarget.ConsoleApp;
using Xunit;

namespace TutorialDeDynamicProxy.InterfaceProxyWithoutTarget.Tests {
    public class DelegateWrapperDeberia {
        [Fact]
        [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
        public void Should_be_able_to_wrap_interface_with_one_method() {
            Func<string, int> length = s => s.Length;
            var wrapped = DelegateWrapper.WrapAs<IAnsweringEngine>(length, null);

            Assert.NotNull(wrapped);
            var i = wrapped.GetAnswer("Answer to Life the Universe and Everything");
            Assert.Equal(42, i);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
        public void Should_be_able_to_write_interface_with_two_methods() {
            Func<string, string, bool> compare = (s1, s2) => s1.Length.Equals(s2.Length);
            Func<string, int> getHashCode = s => s.Length.GetHashCode();

            var comparer = DelegateWrapper.WrapAs<IEqualityComparer<string>>(compare, getHashCode);
            var stringByLength = new Dictionary<string, string>(comparer) {
                { "four", "some string" },
                { "five!", "some other string" }
            };

            Assert.Equal(2, stringByLength.Count);
            var atFive = stringByLength["12345"];
            Assert.Equal("some other string", atFive);
        }
    }
}
