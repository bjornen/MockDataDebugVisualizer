using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests.ComplexTypeDumpersTests
{
    public class ArrayTypeDumperTests
    {
        [Fact]
        public void ShouldBeAbleToDumpInitForThreeStringArray()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var array = new[] { "one", "two", "three" };
            var dumper = new ArrayTypeDumper(array, "TestArray");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveTypeInitilization(codeBuilder);

            Assert.Equal("var testArray_0 = new String[3];", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpInitForOneStringArray()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var array = new string[1];
            var dumper = new ArrayTypeDumper(array, "TestArray");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveTypeInitilization(codeBuilder);

            Assert.Equal("var testArray_0 = new String[1];", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpMembersForThreeStringArray()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var array = new[] { "one", "two", "three" };
            var dumper = new ArrayTypeDumper(array, "TestArray");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveMembers(codeBuilder);

            Assert.Equal("testArray_0[0] = \"one\";\r\ntestArray_0[1] = \"two\";\r\ntestArray_0[2] = \"three\";", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpMembersForThreeStringArrayContainingNull()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var array = new string[]{null, "two", "three"};
            var dumper = new ArrayTypeDumper(array, "TestArray");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveMembers(codeBuilder);

            Assert.Equal("testArray_0[1] = \"two\";\r\ntestArray_0[2] = \"three\";", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpMembersForOneStringArrayContainingNull()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var array = new string[] { null};
            var dumper = new ArrayTypeDumper(array, "TestArray");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveMembers(codeBuilder);

            Assert.Equal(string.Empty, codeBuilder.ToString());
        }
    }
}
