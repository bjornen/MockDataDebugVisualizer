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

            var dumper = DumperBase.GetDumper(i, "ShouldCreateCorrectDumperForValueType");

            Assert.IsType<ValueTypeDumper>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForGuid()
        {
            var guid = Guid.NewGuid();

            var dumper = DumperBase.GetDumper(guid, "ShouldCreateCorrectDumperForGuid");

            Assert.IsType<GuidTypeDumper>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForDateTime()
        {
            var dt = DateTime.Now;

            var dumper = DumperBase.GetDumper(dt, "ShouldCreateCorrectDumperForDateTime");

            Assert.IsType<DateTimeTypeDumper>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForEnum()
        {
            Colors c = Colors.blue;

            var dumper = DumperBase.GetDumper(c, "ShouldCreateCorrectDumperForEnum");

            Assert.IsType<EnumTypeDumper>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForStruct()
        {
            StructObj s = new StructObj();

            var dumper = DumperBase.GetDumper(s, "ShouldCreateCorrectDumperForStruct");

            Assert.IsType<ObjectTypeDumper>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForString()
        {
            string str = "1";

            var dumper = DumperBase.GetDumper(str, "ShouldCreateCorrectDumperForString");

            Assert.IsType<StringTypeDumper>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForArray()
        {
            int[] arr = {1, 2, 3};

            var dumper = DumperBase.GetDumper(arr, "ShouldCreateCorrectDumperForArray");

            Assert.IsType<ArrayTypeDumper>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForDictionary()
        {
            var dic = new Dictionary<string, string>{{"1", "1"}, {"2", "2"}};

            var dumper = DumperBase.GetDumper(dic, "ShouldCreateCorrectDumperForDictionary");

            Assert.IsType<DictionaryTypeDumper>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForEnumerable()
        {
            var list = new List<string>();

            var dumper = DumperBase.GetDumper(list, "ShouldCreateCorrectDumperForValueType");

            Assert.IsType<EnumerableTypeDumper>(dumper);
        }

        [Fact]
        public void ShouldCreateCorrectDumperForObject()
        {
            var o = new object();

            var dumper = DumperBase.GetDumper(o, "ShouldCreateCorrectDumperForObject");

            Assert.IsType<ObjectTypeDumper>(dumper);
        }
    }
}
