using System.Collections.Generic;
using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests.ComplexTypeDumpersTests
{
    public class EnumerableTypeDumperTests
    {
        [Fact]
        public void ShouldBeAbleToDumpValueTypeList()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            
            var list = new List<int> { 1, 2, 3 };

            var dumper = new EnumerableType(list, "ShouldBeAbleToDumpValueTypeList");
            
            var codeBuilder = new CodeBuilder();

            dumper.ResolveTypeInitilization(codeBuilder);

            Assert.Equal("var list_0 = new List<Int32>();", codeBuilder.ToString());
        }
        
        [Fact]
        public void ShouldBeAbleToDumpStringList()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);
            
            var list = new List<string> { "1", "2", "3" };

            var dumper = new EnumerableType(list, "ShouldBeAbleToDumpStringList");
            
            var codeBuilder = new CodeBuilder();

            dumper.ResolveTypeInitilization(codeBuilder);

            Assert.Equal("var list_0 = new List<String>();", codeBuilder.ToString());
        }
        
        [Fact]
        public void ShouldBeAbleToDumpObjectList()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var list = new List<ListTestClass> { new ListTestClass(1), new ListTestClass(2), new ListTestClass(3) };

            var dumper = new EnumerableType(list, "ShouldBeAbleToDumpObjectList");
            
            var codeBuilder = new CodeBuilder();

            dumper.ResolveTypeInitilization(codeBuilder);

            Assert.Equal("var list_0 = new List<ListTestClass>();", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpMembersForValueTypeList()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var list = new List<int> { 1, 2, 3 };

            var dumper = new EnumerableType(list, "ShouldBeAbleToDumpMembersForValueTypeList");
            
            var codeBuilder = new CodeBuilder();

            dumper.ResolveMembers(codeBuilder);

            Assert.Equal("list_0.Add(1);\r\nlist_0.Add(2);\r\nlist_0.Add(3);", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpMembersForStringList()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var list = new List<string> { "1", "2", "3" };

            var dumper = new EnumerableType(list, "ShouldBeAbleToDumpMembersForStringList");
            
            var codeBuilder = new CodeBuilder();

            dumper.ResolveMembers(codeBuilder);

            Assert.Equal("list_0.Add(\"1\");\r\nlist_0.Add(\"2\");\r\nlist_0.Add(\"3\");", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpMembersForObjectList()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var list = new List<ListTestClass> { new ListTestClass(1), new ListTestClass(2), new ListTestClass(3) };

            var dumper = new EnumerableType(list, "ShouldBeAbleToDumpMembersForObjectList");
            
            var codeBuilder = new CodeBuilder();

            dumper.ResolveMembers(codeBuilder);

            Assert.Equal("var listTestClass_1 = (ListTestClass) FormatterServices.GetUninitializedObject(typeof (ListTestClass));\r\nlistTestClass_1.Id = 1;\r\nlist_0.Add(listTestClass_1);\r\nvar listTestClass_2 = (ListTestClass) FormatterServices.GetUninitializedObject(typeof (ListTestClass));\r\nlistTestClass_2.Id = 2;\r\nlist_0.Add(listTestClass_2);\r\nvar listTestClass_3 = (ListTestClass) FormatterServices.GetUninitializedObject(typeof (ListTestClass));\r\nlistTestClass_3.Id = 3;\r\nlist_0.Add(listTestClass_3);", codeBuilder.ToString());
        }

        [Fact]
        public void ShouldBeAbleToDumpMembersForObjectListContainingNull()
        {
            DumperBase.ResetDumper(Visibility.PublicOnly);

            var list = new List<ListTestClass> { new ListTestClass(1), null, new ListTestClass(3) };

            var dumper = new EnumerableType(list, "ShouldBeAbleToDumpMembersForObjectListContainingNull");

            var codeBuilder = new CodeBuilder();

            dumper.ResolveMembers(codeBuilder);

            Assert.Equal("var listTestClass_1 = (ListTestClass) FormatterServices.GetUninitializedObject(typeof (ListTestClass));\r\nlistTestClass_1.Id = 1;\r\nlist_0.Add(listTestClass_1);\r\nvar listTestClass_2 = (ListTestClass) FormatterServices.GetUninitializedObject(typeof (ListTestClass));\r\nlistTestClass_2.Id = 3;\r\nlist_0.Add(listTestClass_2);", codeBuilder.ToString());
        }
    }

    public class ListTestClass
    {
        public ListTestClass(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
