using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests.OneLineInitDumpersTests
{
    public class StringTypeDumperTests
    {
        [Fact]
        public void ShouldBeAbleToDumpString()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new StringType("a string", "ShouldBeAbleToDumpString");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("\"a string\"", codeBuilder.PopInitValue());
        }
        
        [Fact]
        public void ShouldBeAbleToDumpNullString()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new StringType(null, "ShouldBeAbleToDumpNullString");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("null", codeBuilder.PopInitValue());
        }
    }
}
