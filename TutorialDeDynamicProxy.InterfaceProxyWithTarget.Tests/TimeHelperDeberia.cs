using System;
using TutorialDeDynamicProxy.InterfaceProxyWithTarget.ConsoleApp;
using Xunit;

namespace TutorialDeDynamicProxy.InterfaceProxyWithTarget.Tests {
    public class TimeHelperDeberia {
        private readonly ITimeHelper _sut;

        public TimeHelperDeberia() {
            var fix = new TimeFix();
            this._sut = fix.Fix(new TimeHelper());
        }

        [Fact]
        public void GetMinute_should_return_0_for_null() {
            var minute = this._sut.GetMinute(null);
            var second = this._sut.GetSecond(null);
            var hour = this._sut.GetHour(null);

            Assert.Equal(0, minute);
            Assert.Equal(0, second);
            Assert.Equal(0, hour);
        }

        [Fact]
        public void Fixed_GetHour_properly_handles_non_utc_time() {
            var dateTimeOffset = new DateTimeOffset(2009, 10, 11, 09, 32, 11, TimeSpan.FromHours(-4.5));
            var utcTime = dateTimeOffset.ToUniversalTime();
            var noUtcTime = dateTimeOffset.ToString();

            var utcHour = this._sut.GetHour(noUtcTime);
            Assert.Equal(utcTime.Hour, utcHour);
        }

        [Fact]
        public void Fixed_GetMinute_properly_handles_non_utc_time() {
            var dateTimeOffset = new DateTimeOffset(2009, 10, 11, 09, 32, 11, TimeSpan.FromMinutes(45));
            var utcTime = dateTimeOffset.ToUniversalTime();
            var noUtcTime = dateTimeOffset.ToString();

            var utcMinute = this._sut.GetMinute(noUtcTime);
            Assert.Equal(utcTime.Minute, utcMinute);
        }

        [Fact]
        public void Fixed_GetHour_hadles_entries_in_invalid_format() {
            var result = this._sut.GetHour("BOGUS ARGUMENT");
            Assert.Equal(0, result);
        }
    }
}
