using System;
using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests.OneLineInitDumpersTests
{
    public class DateTimeTypeDumperTests
    {
        [Fact]
        public void ShouldBeAbleToDumpDateTime()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            
            var dateTime = new DateTime(2014, 06, 06, 12, 0, 0);

            var dumper = new DateTimeTypeDumper(dateTime, "ShouldBeAbleToDumpDateTime");
            
            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("new DateTime(635376528000000000)", codeBuilder.PopInitValue());
        }
    }
}
