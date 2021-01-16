using System;

namespace TutorialDeDynamicProxy.InterfaceProxyWithTarget.ConsoleApp {
    public sealed class TimeHelper : ITimeHelper {
        public int GetHour(string? dateTime) {
            var time = DateTime.Parse(dateTime!);
            return time.Hour;
        }

        public int GetMinute(string? dateTime) {
            var time = DateTime.Parse(dateTime!);
            return time.Minute;
        }

        public int GetSecond(string? dateTime) {
            var time = DateTime.Parse(dateTime!);
            return time.Second;
        }
    }
}