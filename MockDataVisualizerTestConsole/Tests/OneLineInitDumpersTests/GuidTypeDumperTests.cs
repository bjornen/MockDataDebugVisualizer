using System;
using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests.OneLineInitDumpersTests
{
    public class GuidTypeDumperTests
    {
        [Fact]
        public void ShouldBeAbleToDumpGuid()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var guid = Guid.Parse("672DA660-2431-4532-B0EC-28B483A57D73");

            var dumper = new GuidTypeDumper(guid, "ShouldBeAbleToDumpGuid");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("Guid.Parse(\"672da660-2431-4532-b0ec-28b483a57d73\")", codeBuilder.PopInitValue());
        }
    }
}