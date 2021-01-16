using System.Diagnostics.CodeAnalysis;

namespace TutorialDeDynamicProxy.FreezableApi.Core {
    public class Mascota {
        public virtual int Age { get; set; }
        public virtual bool Deceased { get; set; }

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "Necesario para Resharper")]
        public Mascota(int age, bool deceased) {
            this.Age = age;
            this.Deceased = deceased;
        }
    }
}