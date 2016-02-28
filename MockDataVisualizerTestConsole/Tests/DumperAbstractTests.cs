using System;
using System.Collections.Generic;
using System.IO;
using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers;
using MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers;
using MockDataVisualizerTestConsole.Tests.ComplexTypeDumpersTests.TestObjects;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests
{
    public class DumperAbstractTests
    {
        [Fact]
        public void Skips_types_in_SkipsCfg()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var d = new DirectoryInfo(@"c:\temp\");

            var dump = DumperBase.DumpCode(d, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("In skip types list.", dump);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForValueType()
        {
            int i = 1;

            var dumper = DumperFactory.GetDumper(i, "ShouldCreateCorrectDumperForValueType");

            Assert.IsType<MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers.ValueType>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForGuid()
        {
            var guid = Guid.NewGuid();

            var dumper = DumperFactory.GetDumper(guid, "ShouldCreateCorrectDumperForGuid");

            Assert.IsType<GuidType>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForDateTime()
        {
            var dt = DateTime.Now;

            var dumper = DumperFactory.GetDumper(dt, "ShouldCreateCorrectDumperForDateTime");

            Assert.IsType<DateTimeType>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForEnum()
        {
            Colors c = Colors.blue;

            var dumper = DumperFactory.GetDumper(c, "ShouldCreateCorrectDumperForEnum");

            Assert.IsType<EnumType>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForStruct()
        {
            StructObj s = new StructObj();

            var dumper = DumperFactory.GetDumper(s, "ShouldCreateCorrectDumperForStruct");

            Assert.IsType<ObjectType>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForString()
        {
            string str = "1";

            var dumper = DumperFactory.GetDumper(str, "ShouldCreateCorrectDumperForString");

            Assert.IsType<StringType>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForArray()
        {
            int[] arr = {1, 2, 3};

            var dumper = DumperFactory.GetDumper(arr, "ShouldCreateCorrectDumperForArray");

            Assert.IsType<ArrayType>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForDictionary()
        {
            var dic = new Dictionary<string, string>{{"1", "1"}, {"2", "2"}};

            var dumper = DumperFactory.GetDumper(dic, "ShouldCreateCorrectDumperForDictionary");

            Assert.IsType<DictionaryType>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForEnumerable()
        {
            var list = new List<string>();

            var dumper = DumperFactory.GetDumper(list, "ShouldCreateCorrectDumperForValueType");

            Assert.IsType<EnumerableType>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForObject()
        {
            var o = new object();

            var dumper = DumperFactory.GetDumper(o, "ShouldCreateCorrectDumperForObject");

            Assert.IsType<ObjectType>(dumper);
        }
    }
}
