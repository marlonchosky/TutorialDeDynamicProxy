namespace TutorialDeDynamicProxy.FreezableApi.Core {
    public class Pet {
        public virtual int Age { get; set; }
        public virtual bool Deceased { get; set; }
        public virtual string? Name { get; set; }

        public override string ToString() => $"Name: {this.Name}, Age: {this.Age}, Deceased: {this.Deceased}";
    }
}
