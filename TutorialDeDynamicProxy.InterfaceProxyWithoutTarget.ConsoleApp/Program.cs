namespace TutorialDeDynamicProxy.InterfaceProxyWithoutTarget.ConsoleApp {
    internal class Program {
        private static void Main() {
            IDeepThought d = GetSuperComputer();
        }

        private static IDeepThought GetSuperComputer() {
            throw new System.NotImplementedException();
        }
    }
}
