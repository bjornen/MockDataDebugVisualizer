using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests.OneLineInitDumpersTests
{
    public class ValueTypeDumperTests
    {
        [Fact]
        public void ShouldBeAbleToDumpInt()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new ValueType(9, "ShouldBeAbleToDumpInt");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("9", codeBuilder.PopInitValue());
        }        
        
        [Fact]
        public void ShouldBeAbleToDumpDouble()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new ValueType(9.2D, "ShouldBeAbleToDumpDouble");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("9.2D", codeBuilder.PopInitValue());
        }
        
        [Fact]
        public void ShouldBeAbleToDumpUint()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new ValueType(9U, "ShouldBeAbleToDumpUint");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("9U", codeBuilder.PopInitValue());
        }
        
        [Fact]
        public void ShouldBeAbleToDumpULong()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new ValueType(9UL, "ShouldBeAbleToDumpULong");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("9UL", codeBuilder.PopInitValue());
        }  
      
        [Fact]
        public void ShouldBeAbleToDumpFloat()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new ValueType(9.2F, "ShouldBeAbleToDumpFloat");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("9.2F", codeBuilder.PopInitValue());
        }

        [Fact]
        public void ShouldBeAbleToDumpDecimal()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new ValueType(9.2M, "ShouldBeAbleToDumpDecimal");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("9.2M", codeBuilder.PopInitValue());
        }

        [Fact]
        public void ShouldBeAbleToDumpShort()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new ValueType((short)9, "ShouldBeAbleToDumpShort");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("(short)9", codeBuilder.PopInitValue());
        }

        [Fact]
        public void ShouldBeAbleToDumpUShort()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new ValueType((ushort)9, "ShouldBeAbleToDumpUShort");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("(ushort)9", codeBuilder.PopInitValue());
        }

        [Fact]
        public void ShouldBeAbleToDumpByte()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new ValueType((byte)9, "ShouldBeAbleToDumpByte");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("(byte)9", codeBuilder.PopInitValue());
        }

        [Fact]
        public void ShouldBeAbleToDumpSByte()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var dumper = new ValueType((sbyte)9, "ShouldBeAbleToDumpSByte");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            Assert.Equal("(sbyte)9", codeBuilder.PopInitValue());
        }
    }
}
