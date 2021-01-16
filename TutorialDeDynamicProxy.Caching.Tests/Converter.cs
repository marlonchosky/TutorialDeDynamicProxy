using System.IO;
using System.Text;
using Xunit.Abstractions;

namespace TutorialDeDynamicProxy.Caching.Tests {
    public class Converter : TextWriter {
        private readonly ITestOutputHelper _output;
        
        public Converter(ITestOutputHelper output) => this._output = output;
        
        public override Encoding Encoding => Encoding.UTF8;
        
        public override void WriteLine(string? message) => this._output.WriteLine(message);
        
        public override void WriteLine(string? format, params object?[] args) => this._output.WriteLine(format, args);
    }
}