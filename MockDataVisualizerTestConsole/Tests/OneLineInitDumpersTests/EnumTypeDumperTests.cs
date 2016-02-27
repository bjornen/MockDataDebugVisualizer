using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests.OneLineInitDumpersTests
{
    public class EnumTypeDumperTests
    {
        [Fact]
        public void ShouldBeAbleToDumpEnumFirstValue()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var e = TestEnum.te1;

            var dumper = new EnumType(e, "ShouldBeAbleToDumpEnumFirstValue");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("TestEnum.te1", codeBuilder.PopInitValue());
        }

        [Fact]
        public void ShouldBeAbleToDumpEnumSecondValue()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var e = TestEnum.te2;

            var dumper = new EnumType(e, "ShouldBeAbleToDumpEnumSecondValue");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("TestEnum.te2", codeBuilder.PopInitValue());
        }

        [Fact]
        public void ShouldBeAbleToDumpEnumFirstValueNoNumericValueSpecified()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var e = TestEnum2.te21;

            var dumper = new EnumType(e, "ShouldBeAbleToDumpEnumFirstValueNoNumericValueSpecified");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("TestEnum2.te21", codeBuilder.PopInitValue());
        }
        
        [Fact]
        public void ShouldBeAbleToDumpEnumSecondValueNoNumericValueSpecified()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var e = TestEnum2.te22;

            var dumper = new EnumType(e, "ShouldBeAbleToDumpEnumSecondValueNoNumericValueSpecified");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("TestEnum2.te22", codeBuilder.PopInitValue());
        } 
    }

    internal enum TestEnum
    {
        te1 = 3,
        te2 = 5,
        te3 = 7
    }

    internal enum TestEnum2
    {
        te21,
        te22,
        te23
    }
}
