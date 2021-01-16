namespace TutorialDeDynamicProxy.InterfaceProxyWithTarget.ConsoleApp {
    public interface ITimeHelper {
        int GetHour(string? dateTime);
        int GetMinute(string? dateTime);
        int GetSecond(string? dateTime);
    }
}