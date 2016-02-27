using System;
using System.Collections.Generic;
using System.Reflection;
using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers;
using MockDataVisualizerTestConsole.Tests.ComplexTypeDumpersTests.TestObjects;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests.ComplexTypeDumpersTests
{
    public class ObjectTypeDumperTests
    {
        [Fact]
        public void Can_dump_object()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = Root();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;", dump);
        }

        [Fact]
        public void Can_dump_object_with_private_members()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = RootWithPrivateMembers();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = ComplexRootWithPrivateMembers();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar foo2_1 = new Foo();\r\nfoo2_1.FooValue = \"Foo!\";\r\nvar bar_2 = new Bar();\r\nbar_2.BarValue = \"Bar!\";\r\nfoo2_1.Bar = bar_2;\r\nSetValue(foo2_1, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_1;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = ComplexRootWithPrivateMembersAndInterfaces();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar foo2_1 = new Foo();\r\nfoo2_1.FooValue = \"Foo!\";\r\nvar bar_2 = new Bar();\r\nbar_2.BarValue = \"Bar!\";\r\nfoo2_1.Bar = bar_2;\r\nSetValue(foo2_1, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_1;\r\nvar foo_3 = new Foo();\r\nfoo_3.FooValue = \"IFoo!\";\r\nSetValue(foo_3, \"_privateFooField\", 0);\r\nroot_0.Foo = foo_3;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembers();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar foo2_1 = new Foo();\r\nfoo2_1.FooValue = \"Foo!\";\r\nvar bar_2 = new Bar();\r\nbar_2.BarValue = \"Bar!\";\r\nvar barFoo_3 = new Foo();\r\nbarFoo_3.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(barFoo_3, \"_privateFooField\", 0);\r\nbar_2.BarFoo = barFoo_3;\r\nfoo2_1.Bar = bar_2;\r\nSetValue(foo2_1, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_1;\r\nvar foo_4 = new Foo();\r\nSetValue(foo_4, \"PrivateFooProperty\", \"private foo property\");\r\nfoo_4.FooValue = \"IFoo!\";\r\nSetValue(foo_4, \"_privateFooField\", 999);\r\nroot_0.Foo = foo_4;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReference();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar foo2_1 = new Foo();\r\nfoo2_1.Root = root_0;\r\nfoo2_1.FooValue = \"Foo!\";\r\nvar bar_3 = new Bar();\r\nbar_3.BarValue = \"Bar!\";\r\nvar barFoo_4 = new Foo();\r\nbarFoo_4.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(barFoo_4, \"_privateFooField\", 0);\r\nbar_3.BarFoo = barFoo_4;\r\nfoo2_1.Bar = bar_3;\r\nSetValue(foo2_1, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_1;\r\nvar foo_5 = new Foo();\r\nSetValue(foo_5, \"PrivateFooProperty\", \"private foo property\");\r\nfoo_5.FooValue = \"IFoo!\";\r\nSetValue(foo_5, \"_privateFooField\", 999);\r\nroot_0.Foo = foo_5;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference_and_private_object_member()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMember();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar privateBar_1 = new Bar();\r\nprivateBar_1.BarValue = \"Private bar!! :)\";\r\nSetValue(root_0, \"PrivateBar\", privateBar_1);\r\nvar foo2_2 = new Foo();\r\nfoo2_2.Root = root_0;\r\nfoo2_2.FooValue = \"Foo!\";\r\nvar bar_4 = new Bar();\r\nbar_4.BarValue = \"Bar!\";\r\nvar barFoo_5 = new Foo();\r\nbarFoo_5.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(barFoo_5, \"_privateFooField\", 0);\r\nbar_4.BarFoo = barFoo_5;\r\nfoo2_2.Bar = bar_4;\r\nSetValue(foo2_2, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_2;\r\nvar foo_6 = new Foo();\r\nSetValue(foo_6, \"PrivateFooProperty\", \"private foo property\");\r\nfoo_6.FooValue = \"IFoo!\";\r\nSetValue(foo_6, \"_privateFooField\", 999);\r\nroot_0.Foo = foo_6;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference_and_private_object_member_and_single_argument_enumerable()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMemberAndSingleArgumentEnumerable();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar privateBar_1 = new Bar();\r\nprivateBar_1.BarValue = \"Private bar!! :)\";\r\nSetValue(root_0, \"PrivateBar\", privateBar_1);\r\nvar list_2 = new List<IFoo>();\r\nvar foo_3 = new Foo();\r\nfoo_3.FooValue = \"List foo 1\";\r\nSetValue(foo_3, \"_privateFooField\", 0);\r\nlist_2.Add(foo_3);\r\nvar foo_4 = new Foo();\r\nfoo_4.FooValue = \"List foo 2\";\r\nSetValue(foo_4, \"_privateFooField\", 0);\r\nlist_2.Add(foo_4);\r\nroot_0.Ifoos = list_2;\r\nvar list_5 = new List<Foo>();\r\nvar foo_6 = new Foo();\r\nfoo_6.FooValue = \"List foo 1\";\r\nSetValue(foo_6, \"_privateFooField\", 0);\r\nlist_5.Add(foo_6);\r\nvar foo_7 = new Foo();\r\nfoo_7.FooValue = \"List foo 2\";\r\nSetValue(foo_7, \"_privateFooField\", 0);\r\nlist_5.Add(foo_7);\r\nroot_0.Foos = list_5;\r\nvar foo2_8 = new Foo();\r\nfoo2_8.Root = root_0;\r\nfoo2_8.FooValue = \"Foo!\";\r\nvar bar_10 = new Bar();\r\nbar_10.BarValue = \"Bar!\";\r\nvar barFoo_11 = new Foo();\r\nbarFoo_11.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(barFoo_11, \"_privateFooField\", 0);\r\nbar_10.BarFoo = barFoo_11;\r\nfoo2_8.Bar = bar_10;\r\nSetValue(foo2_8, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_8;\r\nvar foo_12 = new Foo();\r\nSetValue(foo_12, \"PrivateFooProperty\", \"private foo property\");\r\nfoo_12.FooValue = \"IFoo!\";\r\nSetValue(foo_12, \"_privateFooField\", 999);\r\nroot_0.Foo = foo_12;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference_and_private_object_member_and_single_argument_enumerable_and_private_enumerable()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMemberAndSingleArgumentEnumerableAndPrivateEnumerable();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar privateBar_1 = new Bar();\r\nprivateBar_1.BarValue = \"Private bar!! :)\";\r\nSetValue(root_0, \"PrivateBar\", privateBar_1);\r\nvar list_2 = new List<IFoo>();\r\nvar foo_3 = new Foo();\r\nfoo_3.FooValue = \"List foo 1\";\r\nSetValue(foo_3, \"_privateFooField\", 0);\r\nlist_2.Add(foo_3);\r\nvar foo_4 = new Foo();\r\nfoo_4.FooValue = \"List foo 2\";\r\nSetValue(foo_4, \"_privateFooField\", 0);\r\nlist_2.Add(foo_4);\r\nroot_0.Ifoos = list_2;\r\nvar list_5 = new List<Foo>();\r\nvar foo_6 = new Foo();\r\nfoo_6.FooValue = \"List foo 1\";\r\nSetValue(foo_6, \"_privateFooField\", 0);\r\nlist_5.Add(foo_6);\r\nvar foo_7 = new Foo();\r\nfoo_7.FooValue = \"List foo 2\";\r\nSetValue(foo_7, \"_privateFooField\", 0);\r\nlist_5.Add(foo_7);\r\nroot_0.IfooList = list_5;\r\nvar list_8 = new List<Foo>();\r\nvar foo_9 = new Foo();\r\nfoo_9.FooValue = \"List foo 1\";\r\nSetValue(foo_9, \"_privateFooField\", 0);\r\nlist_8.Add(foo_9);\r\nvar foo_10 = new Foo();\r\nfoo_10.FooValue = \"List foo 2\";\r\nSetValue(foo_10, \"_privateFooField\", 0);\r\nlist_8.Add(foo_10);\r\nroot_0.Foos = list_8;\r\nvar list_11 = new List<IFoo>();\r\nvar foo_12 = new Foo();\r\nfoo_12.FooValue = \"List foo 1\";\r\nSetValue(foo_12, \"_privateFooField\", 0);\r\nlist_11.Add(foo_12);\r\nvar foo_13 = new Foo();\r\nfoo_13.FooValue = \"List foo 2\";\r\nSetValue(foo_13, \"_privateFooField\", 0);\r\nlist_11.Add(foo_13);\r\nSetValue(root_0, \"FooList\", list_11);\r\nvar foo2_14 = new Foo();\r\nfoo2_14.Root = root_0;\r\nfoo2_14.FooValue = \"Foo!\";\r\nvar bar_16 = new Bar();\r\nbar_16.BarValue = \"Bar!\";\r\nvar barFoo_17 = new Foo();\r\nbarFoo_17.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(barFoo_17, \"_privateFooField\", 0);\r\nbar_16.BarFoo = barFoo_17;\r\nfoo2_14.Bar = bar_16;\r\nSetValue(foo2_14, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_14;\r\nvar foo_18 = new Foo();\r\nSetValue(foo_18, \"PrivateFooProperty\", \"private foo property\");\r\nfoo_18.FooValue = \"IFoo!\";\r\nSetValue(foo_18, \"_privateFooField\", 999);\r\nroot_0.Foo = foo_18;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_create_complete_create_method()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = Root();

            var dump = DumperBase.DumpCode(root, DumpMode.WrappedCode, Visibility.PrivateAndPublic);

            Assert.Equal("public static Root Createroot_0(){\r\nvar root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nreturn root_0;\r\n}", dump);
        }

        [Fact]
        public void Can_dump_value_type_lists()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = Root();

            root.Numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nvar list_1 = new List<Int32>();\r\nlist_1.Add(1);\r\nlist_1.Add(2);\r\nlist_1.Add(3);\r\nlist_1.Add(4);\r\nlist_1.Add(5);\r\nlist_1.Add(6);\r\nlist_1.Add(7);\r\nroot_0.Numbers = list_1;", dump);
        }

        [Fact]
        public void Can_dump_Array()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = Root();

            root.FooArray = new[] { new Foo { FooValue = "1" }, new Foo { FooValue = "2" }, new Foo { FooValue = "3" }, };

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nvar fooArray_1 = new Foo[3];\r\nvar foo_2 = new Foo();\r\nfoo_2.FooValue = \"1\";\r\nSetValue(foo_2, \"_privateFooField\", 0);\r\nfooArray_1[0] = foo_2;\r\nvar foo_3 = new Foo();\r\nfoo_3.FooValue = \"2\";\r\nSetValue(foo_3, \"_privateFooField\", 0);\r\nfooArray_1[1] = foo_3;\r\nvar foo_4 = new Foo();\r\nfoo_4.FooValue = \"3\";\r\nSetValue(foo_4, \"_privateFooField\", 0);\r\nfooArray_1[2] = foo_4;\r\nroot_0.FooArray = fooArray_1;", dump);
        }

        [Fact]
        public void Can_dump_value_type_array()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = Root();

            root.NumbersArray = new[] { 11, 22, 33 };

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nvar numbersArray_1 = new Int32[3];\r\nnumbersArray_1[0] = 11;\r\nnumbersArray_1[1] = 22;\r\nnumbersArray_1[2] = 33;\r\nroot_0.NumbersArray = numbersArray_1;", dump);
        }

        [Fact]
        public void Can_dump_guid()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = new StructObj();

            root.Unique = Guid.Parse("5182c68d-3459-4bfa-9d14-a3548eb14da8");

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var structobj_0 = new StructObj();\r\nstructobj_0.Unique = Guid.Parse(\"5182c68d-3459-4bfa-9d14-a3548eb14da8\");\r\nstructobj_0.Date = new DateTime(0);", dump);
        }

        [Fact]
        public void Can_dump_DateTime()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = new StructObj();

            root.Unique = Guid.Parse("5182c68d-3459-4bfa-9d14-a3548eb14da8");

            root.Date = new DateTime(635169253125431379);

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var structobj_0 = new StructObj();\r\nstructobj_0.Unique = Guid.Parse(\"5182c68d-3459-4bfa-9d14-a3548eb14da8\");\r\nstructobj_0.Date = new DateTime(635169253125431379);", dump);
        }

        [Fact]
        public void Can_dump_serviced_entity()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var svcObj = new ServiceObject();

            var servicedEntity = new ServicedEntity<IEnumerable<Foo>>(new List<Foo> { new Foo { FooValue = "foo" }, new Foo { FooValue = "bar" } });

            SetValue(svcObj, "_storesUsed", servicedEntity);

            var dump = DumperBase.DumpCode(servicedEntity, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var servicedEntity_0 = new ServicedEntity<IEnumerable<Foo>>();\r\nSetValue(servicedEntity_0, \"IsValid\", true);\r\nvar list_1 = new List<Foo>();\r\nvar foo_2 = new Foo();\r\nfoo_2.FooValue = \"foo\";\r\nSetValue(foo_2, \"_privateFooField\", 0);\r\nlist_1.Add(foo_2);\r\nvar foo_3 = new Foo();\r\nfoo_3.FooValue = \"bar\";\r\nSetValue(foo_3, \"_privateFooField\", 0);\r\nlist_1.Add(foo_3);\r\nSetValue(servicedEntity_0, \"Entity\", list_1);", dump);
        }

        [Fact]
        public void Can_dump_boolean()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var bo = new BoolObject { TheBool = true };

            var dump = DumperBase.DumpCode(bo, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var boolobject_0 = new BoolObject();\r\nboolobject_0.TheBool = true;", dump);
        }

        [Fact]
        public void Can_dump_protected_set()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var root = new Root();

            root.InitilizeProtected();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootId = 0;\r\nvar protectedSetFoo_1 = new Foo();\r\nprotectedSetFoo_1.FooValue = \"Protected Foo\";\r\nSetValue(protectedSetFoo_1, \"_privateFooField\", 0);\r\nSetValue(root_0, \"ProtectedSetFoo\", protectedSetFoo_1);", dump);
        }

        [Fact]
        public void Can_resolve_init_type_name_for_object()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var obj = new Root();

            var initName = ObjectType.ResolveInitTypeName(obj.GetType());

            Assert.Equal("Root", initName);
        }

        [Fact]
        public void Can_dump_enum()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var enumObject = new EnumObject();

            enumObject.Colors1 = Colors.blue;

            SetValue(enumObject, "ColorsWithNumbers", ColorsWithNumbers.red);

            var dump = DumperBase.DumpCode(enumObject, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var enumobject_0 = new EnumObject();\r\nSetValue(enumobject_0, \"ColorsWithNumbers\", ColorsWithNumbers.red);\r\nenumobject_0.Colors1 = Colors.blue;", dump);
        }

        [Fact]
        public void Can_dump_non_public_ctor()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var obj = (PrivateCtorObject)Activator.CreateInstance(typeof(PrivateCtorObject), true);
            obj.Value = "The Value";

            var dump = DumperBase.DumpCode(obj, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var privatectorobject_0 = (PrivateCtorObject) FormatterServices.GetUninitializedObject(typeof (PrivateCtorObject));\r\nprivatectorobject_0.Value = \"The Value\";", dump);
        }

        [Fact]
        public void Can_dump_non_parameterless_ctor()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var obj = new NonParameterlessCtorObj("The values!");

            var dump = DumperBase.DumpCode(obj, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var nonparameterlessctorobj_0 = (NonParameterlessCtorObj) FormatterServices.GetUninitializedObject(typeof (NonParameterlessCtorObj));\r\nnonparameterlessctorobj_0.Value = \"The values!\";", dump);
        }

        [Fact]
        public void Can_dump_List()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var list = new List<Foo> { new Foo { FooValue = "1" }, new Foo { FooValue = "2" } };

            var dump = DumperBase.DumpCode(list, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var list_0 = new List<Foo>();\r\nvar foo_1 = new Foo();\r\nfoo_1.FooValue = \"1\";\r\nSetValue(foo_1, \"_privateFooField\", 0);\r\nlist_0.Add(foo_1);\r\nvar foo_2 = new Foo();\r\nfoo_2.FooValue = \"2\";\r\nSetValue(foo_2, \"_privateFooField\", 0);\r\nlist_0.Add(foo_2);", dump);
        }

        [Fact]
        public void Can_dump_string_string_dictionary()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var dic = new Dictionary<string, string> { { "key", "value" } };

            var dump = DumperBase.DumpCode(dic, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var dictionary_0 = new Dictionary<String, String>();\r\ndictionary_0.Add(\"key\", \"value\");", dump);
        }

        [Fact]
        public void Can_dump_string_object_dictionary()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var dic = new Dictionary<string, Foo> { { "key", new Foo { FooValue = "foo" } } };

            var dump = DumperBase.DumpCode(dic, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var dictionary_0 = new Dictionary<String, Foo>();\r\nvar foo_1 = new Foo();\r\nfoo_1.FooValue = \"foo\";\r\nSetValue(foo_1, \"_privateFooField\", 0);\r\ndictionary_0.Add(\"key\", foo_1);", dump);
        }

        [Fact]
        public void Can_dump_object_object_dictionary()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var dic = new Dictionary<Foo, Foo> { { new Foo { FooValue = "fookey" }, new Foo { FooValue = "foovalue" } } };

            var dump = DumperBase.DumpCode(dic, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var dictionary_0 = new Dictionary<Foo, Foo>();\r\nvar foo_1 = new Foo();\r\nfoo_1.FooValue = \"fookey\";\r\nSetValue(foo_1, \"_privateFooField\", 0);\r\nvar foo_2 = new Foo();\r\nfoo_2.FooValue = \"foovalue\";\r\nSetValue(foo_2, \"_privateFooField\", 0);\r\ndictionary_0.Add(foo_1, foo_2);", dump);
        }

        [Fact]
        public void Can_dump_object_string_dictionary()
        {
            DumperBase.ResetDumper(Visibility.PrivateAndPublic);

            var dic = new Dictionary<Foo, string> { { new Foo { FooValue = "foo" }, "key" } };

            var dump = DumperBase.DumpCode(dic, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var dictionary_0 = new Dictionary<Foo, String>();\r\nvar foo_1 = new Foo();\r\nfoo_1.FooValue = \"foo\";\r\nSetValue(foo_1, \"_privateFooField\", 0);\r\ndictionary_0.Add(foo_1, \"key\");", dump);
        }

        public static Root ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMemberAndSingleArgumentEnumerableAndPrivateEnumerable()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMemberAndSingleArgumentEnumerable();

            var foos = new List<Foo> { new Foo { FooValue = "List foo 1" }, new Foo { FooValue = "List foo 2" } };

            var ifoos = new List<IFoo> { new Foo { FooValue = "List foo 1" }, new Foo { FooValue = "List foo 2" } };

            SetValue(root, "FooList", ifoos);

            root.IfooList = foos;

            return root;
        }

        private static Root ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMemberAndSingleArgumentEnumerable()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMember();

            var foos = new List<Foo> { new Foo { FooValue = "List foo 1" }, new Foo { FooValue = "List foo 2" } };

            var ifoos = new List<IFoo> { new Foo { FooValue = "List foo 1" }, new Foo { FooValue = "List foo 2" } };

            root.Foos = foos;

            root.Ifoos = ifoos;

            return root;
        }

        private static Root ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMember()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReference();

            var privateBar = new Bar { BarValue = "Private bar!! :)" };

            SetValue(root, "PrivateBar", privateBar);

            return root;
        }

        private static Root ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReference()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembers();

            root.Foo2.Root = root;

            return root;
        }

        private static Root ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembers()
        {
            var root = ComplexRootWithPrivateMembersAndInterfaces();
            root.Foo2.Bar.BarFoo = new Foo { FooValue = "Root-Foo2-Bar-BarFoo" };
            SetValue(root.Foo, "PrivateFooProperty", "private foo property");
            SetValue(root.Foo, "_privateFooField", 999);
            return root;
        }

        private static Root ComplexRootWithPrivateMembersAndInterfaces()
        {
            var root = ComplexRootWithPrivateMembers();
            root.Foo = new Foo { FooValue = "IFoo!" };
            return root;
        }

        private static Root ComplexRootWithPrivateMembers()
        {
            var root = RootWithPrivateMembers();
            root.Foo2 = new Foo { FooValue = "Foo!", Bar = new Bar { BarValue = "Bar!" } };
            return root;
        }

        private static Root RootWithPrivateMembers()
        {
            var root = Root();

            root.InitializePrivates();

            return root;
        }

        public static Root Root()
        {
            var root = new Root
            {
                RootName = "The Root!",
                RootId = 1
            };
            return root;
        }

        public static void SetValue<T>(object obj, string propName, T val)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var t = obj.GetType();

            if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) != null)
            {
                t.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, obj, new object[] { val });
            }
            else
            {
                FieldInfo fi = null;
                while (fi == null && t != null)
                {
                    fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    t = t.BaseType;
                }
                if (fi == null)
                    throw new ArgumentOutOfRangeException("propName", string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));
                fi.SetValue(obj, val);
            }
        }
    }
}
