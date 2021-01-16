namespace TutorialDeDynamicProxy.FreezableApi.Core {
    public interface IFreezable {
        bool IsFrozen { get; }
        void Freeze();
    }
}