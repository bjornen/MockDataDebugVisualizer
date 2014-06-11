using System;
using System.Collections.Generic;
using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests.ComplexTypeDumpersTests
{
    public class DictionaryTypeDumperTests
    {
        [Fact]
        public void ShouldBeAbleToDumpStringStringDictionaryInitCode()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var dic = new Dictionary<string, string> {{"key1", "value1"}, {"key2", "value2"}};
            var dumper = new DictionaryTypeDumper(dic, "TestDic");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveTypeInitilization(codeBuilder);

            Assert.Equal("var dictionary_0 = new Dictionary<String, String>();", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpStringObjectDictionaryInitCode()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var dic = new Dictionary<string, object> { { "key1", new Object() }, { "key2", "value2" } };
            var dumper = new DictionaryTypeDumper(dic, "TestDic");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveTypeInitilization(codeBuilder);

            Assert.Equal("var dictionary_0 = new Dictionary<String, Object>();", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpObjectStringDictionaryInitCode()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var dic = new Dictionary<object, string> { { new Object(), "value1" }, { "key2", "value2" } };
            var dumper = new DictionaryTypeDumper(dic, "TestDic");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveTypeInitilization(codeBuilder);

            Assert.Equal("var dictionary_0 = new Dictionary<Object, String>();", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpObjectObjectDictionaryInitCode()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var dic = new Dictionary<object, object> { { new Object(), new Object() }, { "key2", new Object() } };
            var dumper = new DictionaryTypeDumper(dic, "TestDic");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveTypeInitilization(codeBuilder);

            Assert.Equal("var dictionary_0 = new Dictionary<Object, Object>();", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpMembersForObjectObjectDictionary()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var dic = new Dictionary<object, object> { { new Object(), new Object() }, { "key2", new Object() } };
            var dumper = new DictionaryTypeDumper(dic, "TestDic");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveMembers(codeBuilder);

            Assert.Equal("var object_1 = new Object();\r\nvar object_2 = new Object();\r\ndictionary_0.Add(object_1, object_2);\r\nvar object_3 = new Object();\r\ndictionary_0.Add(\"key2\", object_3);", codeBuilder.ToString());
        }        
        
        [Fact]
        public void ShouldBeAbleToDumpMembersForStringStringDictionary()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var dic = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } };
            var dumper = new DictionaryTypeDumper(dic, "TestDic");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveMembers(codeBuilder);

            Assert.Equal("dictionary_0.Add(\"key1\", \"value1\");\r\ndictionary_0.Add(\"key2\", \"value2\");", codeBuilder.ToString());
        }        
        
        [Fact]
        public void ShouldBeAbleToDumpMembersForIntStringDictionary()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            var dic = new Dictionary<int, string> { { 1, "value1" }, { 2, "value2" } };
            var dumper = new DictionaryTypeDumper(dic, "TestDic");
            var codeBuilder = new CodeBuilder();

            dumper.ResolveMembers(codeBuilder);

            Assert.Equal("dictionary_0.Add(1, \"value1\");\r\ndictionary_0.Add(2, \"value2\");", codeBuilder.ToString());
        }
    }
}
