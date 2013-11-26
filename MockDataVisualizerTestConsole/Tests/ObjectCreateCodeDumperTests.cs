using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ICA.MittICA.Recipes.DataContext;
using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers;
using MockDataVisualizerTestConsole.Tests.TestObjects;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests
{
    public class ObjectCreateCodeDumperTests
    {
        [Fact]
        public void Skips_types_in_SkipsCfg()
        {
            var d = new DirectoryInfo(@"c:\temp\");

            var dump = DumperBase.DumpCode(d, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("In skip types list.", dump);
        }

        [Fact]
        public void Can_dump_object()
        {
            var root = Root();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;", dump);
        }

        [Fact]
        public void Can_dump_object_with_private_members()
        {
            var root = RootWithPrivateMembers();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members()
        {
            var root = ComplexRootWithPrivateMembers();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar foo2_1 = new Foo();\r\nfoo2_1.FooValue = \"Foo!\";\r\nvar bar_2 = new Bar();\r\nbar_2.BarValue = \"Bar!\";\r\nfoo2_1.Bar = bar_2;\r\nSetValue(foo2_1, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_1;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces()
        {
            var root = ComplexRootWithPrivateMembersAndInterfaces();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar foo2_1 = new Foo();\r\nfoo2_1.FooValue = \"Foo!\";\r\nvar bar_2 = new Bar();\r\nbar_2.BarValue = \"Bar!\";\r\nfoo2_1.Bar = bar_2;\r\nSetValue(foo2_1, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_1;\r\nvar foo_3 = new Foo();\r\nfoo_3.FooValue = \"IFoo!\";\r\nSetValue(foo_3, \"_privateFooField\", 0);\r\nroot_0.Foo = foo_3;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembers();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar foo2_1 = new Foo();\r\nfoo2_1.FooValue = \"Foo!\";\r\nvar bar_2 = new Bar();\r\nbar_2.BarValue = \"Bar!\";\r\nvar barFoo_3 = new Foo();\r\nbarFoo_3.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(barFoo_3, \"_privateFooField\", 0);\r\nbar_2.BarFoo = barFoo_3;\r\nfoo2_1.Bar = bar_2;\r\nSetValue(foo2_1, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_1;\r\nvar foo_4 = new Foo();\r\nSetValue(foo_4, \"PrivateFooProperty\", \"private foo property\");\r\nfoo_4.FooValue = \"IFoo!\";\r\nSetValue(foo_4, \"_privateFooField\", 999);\r\nroot_0.Foo = foo_4;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReference();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar foo2_1 = new Foo();\r\nfoo2_1.Root = root_0;\r\nfoo2_1.FooValue = \"Foo!\";\r\nvar bar_3 = new Bar();\r\nbar_3.BarValue = \"Bar!\";\r\nvar barFoo_4 = new Foo();\r\nbarFoo_4.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(barFoo_4, \"_privateFooField\", 0);\r\nbar_3.BarFoo = barFoo_4;\r\nfoo2_1.Bar = bar_3;\r\nSetValue(foo2_1, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_1;\r\nvar foo_5 = new Foo();\r\nSetValue(foo_5, \"PrivateFooProperty\", \"private foo property\");\r\nfoo_5.FooValue = \"IFoo!\";\r\nSetValue(foo_5, \"_privateFooField\", 999);\r\nroot_0.Foo = foo_5;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference_and_private_object_member()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMember();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar privateBar_1 = new Bar();\r\nprivateBar_1.BarValue = \"Private bar!! :)\";\r\nSetValue(root_0, \"PrivateBar\", privateBar_1);\r\nvar foo2_2 = new Foo();\r\nfoo2_2.Root = root_0;\r\nfoo2_2.FooValue = \"Foo!\";\r\nvar bar_4 = new Bar();\r\nbar_4.BarValue = \"Bar!\";\r\nvar barFoo_5 = new Foo();\r\nbarFoo_5.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(barFoo_5, \"_privateFooField\", 0);\r\nbar_4.BarFoo = barFoo_5;\r\nfoo2_2.Bar = bar_4;\r\nSetValue(foo2_2, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_2;\r\nvar foo_6 = new Foo();\r\nSetValue(foo_6, \"PrivateFooProperty\", \"private foo property\");\r\nfoo_6.FooValue = \"IFoo!\";\r\nSetValue(foo_6, \"_privateFooField\", 999);\r\nroot_0.Foo = foo_6;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference_and_private_object_member_and_single_argument_enumerable()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMemberAndSingleArgumentEnumerable();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar privateBar_1 = new Bar();\r\nprivateBar_1.BarValue = \"Private bar!! :)\";\r\nSetValue(root_0, \"PrivateBar\", privateBar_1);\r\nvar list_2 = new List<IFoo>();\r\nvar foo_3 = new Foo();\r\nfoo_3.FooValue = \"List foo 1\";\r\nSetValue(foo_3, \"_privateFooField\", 0);\r\nlist_2.Add(foo_3);\r\nvar foo_4 = new Foo();\r\nfoo_4.FooValue = \"List foo 2\";\r\nSetValue(foo_4, \"_privateFooField\", 0);\r\nlist_2.Add(foo_4);\r\nroot_0.Ifoos = list_2;\r\nvar list_5 = new List<Foo>();\r\nvar foo_6 = new Foo();\r\nfoo_6.FooValue = \"List foo 1\";\r\nSetValue(foo_6, \"_privateFooField\", 0);\r\nlist_5.Add(foo_6);\r\nvar foo_7 = new Foo();\r\nfoo_7.FooValue = \"List foo 2\";\r\nSetValue(foo_7, \"_privateFooField\", 0);\r\nlist_5.Add(foo_7);\r\nroot_0.Foos = list_5;\r\nvar foo2_8 = new Foo();\r\nfoo2_8.Root = root_0;\r\nfoo2_8.FooValue = \"Foo!\";\r\nvar bar_10 = new Bar();\r\nbar_10.BarValue = \"Bar!\";\r\nvar barFoo_11 = new Foo();\r\nbarFoo_11.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(barFoo_11, \"_privateFooField\", 0);\r\nbar_10.BarFoo = barFoo_11;\r\nfoo2_8.Bar = bar_10;\r\nSetValue(foo2_8, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_8;\r\nvar foo_12 = new Foo();\r\nSetValue(foo_12, \"PrivateFooProperty\", \"private foo property\");\r\nfoo_12.FooValue = \"IFoo!\";\r\nSetValue(foo_12, \"_privateFooField\", 999);\r\nroot_0.Foo = foo_12;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference_and_private_object_member_and_single_argument_enumerable_and_private_enumerable()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMemberAndSingleArgumentEnumerableAndPrivateEnumerable();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nvar privateBar_1 = new Bar();\r\nprivateBar_1.BarValue = \"Private bar!! :)\";\r\nSetValue(root_0, \"PrivateBar\", privateBar_1);\r\nvar list_2 = new List<IFoo>();\r\nvar foo_3 = new Foo();\r\nfoo_3.FooValue = \"List foo 1\";\r\nSetValue(foo_3, \"_privateFooField\", 0);\r\nlist_2.Add(foo_3);\r\nvar foo_4 = new Foo();\r\nfoo_4.FooValue = \"List foo 2\";\r\nSetValue(foo_4, \"_privateFooField\", 0);\r\nlist_2.Add(foo_4);\r\nroot_0.Ifoos = list_2;\r\nvar list_5 = new List<Foo>();\r\nvar foo_6 = new Foo();\r\nfoo_6.FooValue = \"List foo 1\";\r\nSetValue(foo_6, \"_privateFooField\", 0);\r\nlist_5.Add(foo_6);\r\nvar foo_7 = new Foo();\r\nfoo_7.FooValue = \"List foo 2\";\r\nSetValue(foo_7, \"_privateFooField\", 0);\r\nlist_5.Add(foo_7);\r\nroot_0.IfooList = list_5;\r\nvar list_8 = new List<Foo>();\r\nvar foo_9 = new Foo();\r\nfoo_9.FooValue = \"List foo 1\";\r\nSetValue(foo_9, \"_privateFooField\", 0);\r\nlist_8.Add(foo_9);\r\nvar foo_10 = new Foo();\r\nfoo_10.FooValue = \"List foo 2\";\r\nSetValue(foo_10, \"_privateFooField\", 0);\r\nlist_8.Add(foo_10);\r\nroot_0.Foos = list_8;\r\nvar list_11 = new List<IFoo>();\r\nvar foo_12 = new Foo();\r\nfoo_12.FooValue = \"List foo 1\";\r\nSetValue(foo_12, \"_privateFooField\", 0);\r\nlist_11.Add(foo_12);\r\nvar foo_13 = new Foo();\r\nfoo_13.FooValue = \"List foo 2\";\r\nSetValue(foo_13, \"_privateFooField\", 0);\r\nlist_11.Add(foo_13);\r\nSetValue(root_0, \"FooList\", list_11);\r\nvar foo2_14 = new Foo();\r\nfoo2_14.Root = root_0;\r\nfoo2_14.FooValue = \"Foo!\";\r\nvar bar_16 = new Bar();\r\nbar_16.BarValue = \"Bar!\";\r\nvar barFoo_17 = new Foo();\r\nbarFoo_17.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(barFoo_17, \"_privateFooField\", 0);\r\nbar_16.BarFoo = barFoo_17;\r\nfoo2_14.Bar = bar_16;\r\nSetValue(foo2_14, \"_privateFooField\", 0);\r\nroot_0.Foo2 = foo2_14;\r\nvar foo_18 = new Foo();\r\nSetValue(foo_18, \"PrivateFooProperty\", \"private foo property\");\r\nfoo_18.FooValue = \"IFoo!\";\r\nSetValue(foo_18, \"_privateFooField\", 999);\r\nroot_0.Foo = foo_18;\r\nSetValue(root_0, \"_privateField\", \"The private field\");\r\nSetValue(root_0, \"_backing\", \"The private backing field\");", dump);
        }        
        
        [Fact]
        public void Can_create_complete_create_method()
        {
            var root = Root();

            var dump = DumperBase.DumpCode(root, DumpMode.WrappedCode, Visibility.PrivateAndPublic);

            Assert.Equal("public static Root Createroot_0(){\r\nvar root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nreturn root_0;\r\n}", dump);
        }

        [Fact]
        public void Can_dump_value_type_lists()
        {
            var root = Root();

            root.Numbers = new List<int>{1,2,3,4,5,6,7};

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nvar list_1 = new List<Int32>();\r\nlist_1.Add(1);\r\nlist_1.Add(2);\r\nlist_1.Add(3);\r\nlist_1.Add(4);\r\nlist_1.Add(5);\r\nlist_1.Add(6);\r\nlist_1.Add(7);\r\nroot_0.Numbers = list_1;", dump);
        }

        [Fact]
        public void Can_dump_Array()
        {
            var root = Root();

            root.FooArray = new[] {new Foo {FooValue = "1"}, new Foo {FooValue = "2"}, new Foo {FooValue = "3"},};

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nvar fooArray_1 = new Foo[3];\r\nvar foo_2 = new Foo();\r\nfoo_2.FooValue = \"1\";\r\nSetValue(foo_2, \"_privateFooField\", 0);\r\nfooArray_1[0] = foo_2;\r\nvar foo_3 = new Foo();\r\nfoo_3.FooValue = \"2\";\r\nSetValue(foo_3, \"_privateFooField\", 0);\r\nfooArray_1[1] = foo_3;\r\nvar foo_4 = new Foo();\r\nfoo_4.FooValue = \"3\";\r\nSetValue(foo_4, \"_privateFooField\", 0);\r\nfooArray_1[2] = foo_4;\r\nroot_0.FooArray = fooArray_1;", dump);
        }

        [Fact]
        public void Can_dump_value_type_array()
        {
            var root = Root();

            root.NumbersArray = new[] { 11, 22, 33 };

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootName = \"The Root!\";\r\nroot_0.RootId = 1;\r\nvar numbersArray_1 = new Int32[3];\r\nnumbersArray_1[0] = 11;\r\nnumbersArray_1[1] = 22;\r\nnumbersArray_1[2] = 33;\r\nroot_0.NumbersArray = numbersArray_1;", dump);
        }

        [Fact]
        public void Can_dump_guid()
        {
            var root = new StructObj();

            root.Unique = Guid.Parse("5182c68d-3459-4bfa-9d14-a3548eb14da8");

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var structobj_0 = new StructObj();\r\nstructobj_0.Unique = Guid.Parse(\"5182c68d-3459-4bfa-9d14-a3548eb14da8\");\r\nstructobj_0.Date = new DateTime(0);", dump);
        }

        [Fact]
        public void Can_dump_DateTime()
        {
            var root = new StructObj();

            root.Unique = Guid.Parse("5182c68d-3459-4bfa-9d14-a3548eb14da8");

            root.Date = new DateTime(635169253125431379);

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var structobj_0 = new StructObj();\r\nstructobj_0.Unique = Guid.Parse(\"5182c68d-3459-4bfa-9d14-a3548eb14da8\");\r\nstructobj_0.Date = new DateTime(635169253125431379);", dump);
        }

        [Fact]
        public void Can_dump_serviced_entity()
        {
            var svcObj = new ServiceObject();

            var servicedEntity = new ServicedEntity<IEnumerable<Foo>>(new List<Foo> {new Foo {FooValue = "foo"}, new Foo {FooValue = "bar"}});

            SetValue(svcObj, "_storesUsed", servicedEntity);

            var dump = DumperBase.DumpCode(servicedEntity, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var servicedEntity_0 = new ServicedEntity<IEnumerable<Foo>>();\r\nSetValue(servicedEntity_0, \"IsValid\", true);\r\nvar list_1 = new List<Foo>();\r\nvar foo_2 = new Foo();\r\nfoo_2.FooValue = \"foo\";\r\nSetValue(foo_2, \"_privateFooField\", 0);\r\nlist_1.Add(foo_2);\r\nvar foo_3 = new Foo();\r\nfoo_3.FooValue = \"bar\";\r\nSetValue(foo_3, \"_privateFooField\", 0);\r\nlist_1.Add(foo_3);\r\nSetValue(servicedEntity_0, \"Entity\", list_1);", dump);
        }

        [Fact]
        public void Can_dump_boolean()
        {
            var bo = new BoolObject {TheBool = true};

            var dump = DumperBase.DumpCode(bo, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var boolobject_0 = new BoolObject();\r\nboolobject_0.TheBool = true;", dump);
        }

        [Fact]
        public void Can_dump_protected_set()
        {
            var root = new Root();
            
            root.InitilizeProtected();

            var dump = DumperBase.DumpCode(root, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootId = 0;\r\nvar protectedSetFoo_1 = new Foo();\r\nprotectedSetFoo_1.FooValue = \"Protected Foo\";\r\nSetValue(protectedSetFoo_1, \"_privateFooField\", 0);\r\nSetValue(root_0, \"ProtectedSetFoo\", protectedSetFoo_1);", dump);
        }

        [Fact]
        public void Can_resolve_init_type_name_for_object()
        {
            var obj = new Root();

            var initName = ObjectTypeDumper.ResolveInitTypeName(obj.GetType());

            Assert.Equal("Root", initName);
        }

        [Fact]
        public void Can_dump_enum()
        {
            var enumObject = new EnumObject();

            enumObject.Colors1 = Colors.blue;

            //enumObject.ColorsWithNumbers = ColorsWithNumbers.red;
            SetValue(enumObject, "ColorsWithNumbers", ColorsWithNumbers.red);

            var dump = DumperBase.DumpCode(enumObject, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var enumobject_0 = new EnumObject();\r\nSetValue(enumobject_0, \"ColorsWithNumbers\", ColorsWithNumbers.red);\r\nenumobject_0.Colors1 = Colors.blue;", dump);
        }

        [Fact]
        public void Can_dump_non_public_ctor()
        {
            var obj = (PrivateCtorObject)Activator.CreateInstance(typeof(PrivateCtorObject), true);
            obj.Value = "The Value";

            var dump = DumperBase.DumpCode(obj, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var privatectorobject_0 = (PrivateCtorObject) FormatterServices.GetUninitializedObject(typeof (PrivateCtorObject));\r\nprivatectorobject_0.Value = \"The Value\";", dump);
        }

        [Fact]
        public void Can_dump_non_parameterless_ctor()
        {
            var obj = new NonParameterlessCtorObj("The values!");

            var dump = DumperBase.DumpCode(obj, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var nonparameterlessctorobj_0 = (NonParameterlessCtorObj) FormatterServices.GetUninitializedObject(typeof (NonParameterlessCtorObj));\r\nnonparameterlessctorobj_0.Value = \"The values!\";", dump);
        }

        //[Fact]
        //public void Can_dump_exception()
        //{
        //    //var se = new ServicedEntity<IEnumerable<StoreUsedStats>>(new Exception("Fel..."));

        //    var se = new Exception("Fel..");

        //    var dumper = Dumper.DumpInitlizationCode(se);

        //    Assert.Equal("ServicedEntity<IEnumerable<StoreUsedStats>>", dumper);
        //}

        [Fact]
        public void Can_dump_List()
        {
            var list = new List<Foo> {new Foo {FooValue = "1"}, new Foo {FooValue = "2"}};

            var dump = DumperBase.DumpCode(list, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var list_0 = new List<Foo>();\r\nvar foo_1 = new Foo();\r\nfoo_1.FooValue = \"1\";\r\nSetValue(foo_1, \"_privateFooField\", 0);\r\nlist_0.Add(foo_1);\r\nvar foo_2 = new Foo();\r\nfoo_2.FooValue = \"2\";\r\nSetValue(foo_2, \"_privateFooField\", 0);\r\nlist_0.Add(foo_2);", dump);
        }

        [Fact]
        public void Can_dump_string_string_dictionary()
        {
            var dic = new Dictionary<string, string> {{"key", "value"}};

            var dump = DumperBase.DumpCode(dic, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

              Assert.Equal("var dictionary_0 = new Dictionary<String, String>();\r\ndictionary_0.Add(\"key\", \"value\");", dump);
        }

        [Fact]
        public void Can_dump_string_object_dictionary()
        {
            var dic = new Dictionary<string, Foo> { { "key", new Foo{FooValue = "foo" }} };

            var dump = DumperBase.DumpCode(dic, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var dictionary_0 = new Dictionary<String, Foo>();\r\nvar foo_1 = new Foo();\r\nfoo_1.FooValue = \"foo\";\r\nSetValue(foo_1, \"_privateFooField\", 0);\r\ndictionary_0.Add(\"key\", foo_1);", dump);
        }

        [Fact]
        public void Can_dump_object_object_dictionary()
        {
            var dic = new Dictionary<Foo, Foo> { { new Foo { FooValue = "fookey" }, new Foo { FooValue = "foovalue" } } };

            var dump = DumperBase.DumpCode(dic, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var dictionary_0 = new Dictionary<Foo, Foo>();\r\nvar foo_1 = new Foo();\r\nfoo_1.FooValue = \"fookey\";\r\nSetValue(foo_1, \"_privateFooField\", 0);\r\nvar foo_2 = new Foo();\r\nfoo_2.FooValue = \"foovalue\";\r\nSetValue(foo_2, \"_privateFooField\", 0);\r\ndictionary_0.Add(foo_1, foo_2);", dump);
        }

        [Fact]
        public void Can_dump_object_string_dictionary()
        {
            var dic = new Dictionary<Foo, string> { { new Foo { FooValue = "foo" }, "key" } };

            var dump = DumperBase.DumpCode(dic, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var dictionary_0 = new Dictionary<Foo, String>();\r\nvar foo_1 = new Foo();\r\nfoo_1.FooValue = \"foo\";\r\nSetValue(foo_1, \"_privateFooField\", 0);\r\ndictionary_0.Add(foo_1, \"key\");", dump);
        }

        [Fact]
        public void Can_dump_recipe()
        {
            var recipe = CreateFakeRecipe();

            var dump = DumperBase.DumpCode(recipe, DumpMode.CodeOnly, Visibility.PrivateAndPublic);

            Assert.Equal("var recipe_0 = new Recipe();\r\nvar list_1 = new List<TextGroup>();\r\nvar textGroup_2 = new TextGroup();\r\ntextGroup_2.TextTypeId = 3;\r\ntextGroup_2.TextId = 0;\r\nvar text_3 = new Text();\r\ntext_3.StopDate = new DateTime(0);\r\ntext_3.StartDate = new DateTime(0);\r\ntext_3.Id = 111;\r\ntext_3.Header = \"Header\";\r\ntext_3.Body = \"Pufftext\";\r\ntextGroup_2.Text = text_3;\r\ntextGroup_2.RecipeId = 1;\r\nlist_1.Add(textGroup_2);\r\nvar textGroup_4 = new TextGroup();\r\ntextGroup_4.TextTypeId = 1;\r\ntextGroup_4.TextId = 0;\r\nvar text_5 = new Text();\r\ntext_5.StopDate = new DateTime(0);\r\ntext_5.StartDate = new DateTime(0);\r\ntext_5.Id = 111;\r\ntext_5.Header = \"Header\";\r\ntext_5.Body = \"<ol>\r\n<li>Dela, k&auml;rna ur och strimla chilin. Skala och finhacka vitl&ouml;ken. Sk&auml;r fikonen i grova bitar.</li>\r\n<li>Putsa och sk&auml;r k&ouml;ttet i grytbitar. Strimla baconet.</li>\r\n<li>Bryn k&ouml;tt och bacon i omg&aring;ngar i olja i en stekpanna.</li>\r\n<li>Fr&auml;s tomatpur&eacute;, vitl&ouml;k, chilipulver och spiskummin i olja i en stor gryta/kastrull n&aring;gon minut. L&auml;gg i k&ouml;tt, bacon, strimlad chili, fikon, inlagd jalape&ntilde;o, torkad chilifrukt, fond, &ouml;l, vin, lagerblad, paprikapulver, oregano och koriander. Tills&auml;tt lite vatten om inte allt k&ouml;tt &auml;r t&auml;ckt.</li>\r\n<li>Koka upp och l&aring;t sm&aring;koka under lock tills k&ouml;ttet faller is&auml;r, det tar minst 3&ndash;4 timmar. R&ouml;r d&aring; och d&aring;, s&aring; att det inte br&auml;nner fast i bottnen. Smaka av med salt. Servera med tillbeh&ouml;ren inklusive <a href=\"http://www.ica.se/recept?recipeid=715431\">Godaste guacamolen</a>.</li>\r\n</ol>\r\n<h3>F&ouml;r alla</h3>\r\n<p>G&ouml;r glutenfri: v&auml;lj &ouml;l fritt fr&aring;n gluten eller med l&aring;g glutenhalt. (Se t ex lista fr&aring;n SLV.) Servera med l&auml;mpliga tillbeh&ouml;r.<br />G&ouml;r laktosfri: kontrollera inneh&aring;llet i fonden. Servera med l&auml;mpliga tillbeh&ouml;r.<br />G&ouml;r mj&ouml;lkproteinfri: kontrollera inneh&aring;llet i fonden. Servera med l&auml;mpliga tillbeh&ouml;r.<br />\";\r\ntextGroup_4.Text = text_5;\r\ntextGroup_4.RecipeId = 1;\r\nlist_1.Add(textGroup_4);\r\nrecipe_0.Texts = list_1;\r\nrecipe_0.StopDate = new DateTime(0);\r\nrecipe_0.Status = 2;\r\nrecipe_0.StartDate = new DateTime(0);\r\nrecipe_0.ShowServingsSelector = false;\r\nvar recipeParts_6 = new PartOfRecipe[1];\r\nvar partOfRecipe_7 = new PartOfRecipe();\r\npartOfRecipe_7.RecipeId = 1M;\r\npartOfRecipe_7.Order = 0M;\r\nvar list_8 = new List<IngredientData>();\r\nlist_8.Add(textGroup_2);\r\npartOfRecipe_7.IngredientDataRows = list_8;\r\npartOfRecipe_7.Id = 0;\r\npartOfRecipe_7.Heading = \"Del av recept\";\r\nrecipeParts_6[0] = partOfRecipe_7;\r\nrecipe_0.RecipeParts = recipeParts_6;\r\nvar recipeMedia_10 = new RecipeMedia();\r\nrecipeMedia_10.Status = MediaStatus.New;\r\nrecipeMedia_10.RecipeId = 0;\r\nrecipeMedia_10.NumberOfVotes = 0;\r\nrecipeMedia_10.ImageId = 1111;\r\nrecipeMedia_10.Id = 0;\r\nrecipeMedia_10.EditorialImage = false;\r\nrecipe_0.RecipeMedia = recipeMedia_10;\r\nrecipe_0.Portions = \"4\";\r\nrecipe_0.Name = \"Testreceptet\";\r\nrecipe_0.IsPersonal = false;\r\nrecipe_0.IsBuffe = false;\r\nrecipe_0.Id = 1;\r\nrecipe_0.Guid = Guid.Parse(\"00000000-0000-0000-0000-000000000000\");\r\nrecipe_0.DateModified = new DateTime(0);\r\nrecipe_0.CustomerId = 0M;\r\nvar list_11 = new List<Recipe_Category>();\r\nvar recipe_Category_12 = new Recipe_Category();\r\nrecipe_Category_12.SubcategoryId = 0;\r\nvar subCategory_13 = new SubCategory();\r\nsubCategory_13.SubcategoryId = 0;\r\nsubCategory_13.Status = PublishedStatus.Arkiverad;\r\nsubCategory_13.State = 0;\r\nsubCategory_13.ShowImage = 0;\r\nsubCategory_13.ParentSubcategory = 0;\r\nsubCategory_13.Name = \"Under30Min\";\r\nsubCategory_13.LatestChange = new DateTime(0);\r\nsubCategory_13.ImageId = 0;\r\nsubCategory_13.Id = 0;\r\nvar _category_14 = new Category();\r\nvar list_15 = new List<SubCategory>();\r\n_category_14.Subcategory = list_15;\r\n_category_14.Status = 0;\r\n_category_14.Name = \"Under30Min\";\r\n_category_14.LatestChange = new DateTime(0);\r\n_category_14.Id = 146;\r\nSetValue(subCategory_13, \"_category\", _category_14);\r\nrecipe_Category_12.SubCategory = subCategory_13;\r\nrecipe_Category_12.RecipeId = 0;\r\nrecipe_Category_12.CategoryId = 5;\r\nlist_11.Add(recipe_Category_12);\r\nvar recipe_Category_16 = new Recipe_Category();\r\nrecipe_Category_16.SubcategoryId = 0;\r\nvar subCategory_17 = new SubCategory();\r\nsubCategory_17.SubcategoryId = 0;\r\nsubCategory_17.Status = PublishedStatus.Arkiverad;\r\nsubCategory_17.State = 0;\r\nsubCategory_17.ShowImage = 0;\r\nsubCategory_17.ParentSubcategory = 0;\r\nsubCategory_17.Name = \"MediumLevel\";\r\nsubCategory_17.LatestChange = new DateTime(0);\r\nsubCategory_17.ImageId = 0;\r\nsubCategory_17.Id = 0;\r\nvar _category_18 = new Category();\r\nvar list_19 = new List<SubCategory>();\r\n_category_18.Subcategory = list_19;\r\n_category_18.Status = 0;\r\n_category_18.Name = \"MediumLevel\";\r\n_category_18.LatestChange = new DateTime(0);\r\n_category_18.Id = 62;\r\nSetValue(subCategory_17, \"_category\", _category_18);\r\nrecipe_Category_16.SubCategory = subCategory_17;\r\nrecipe_Category_16.RecipeId = 0;\r\nrecipe_Category_16.CategoryId = 6;\r\nlist_11.Add(recipe_Category_16);\r\nvar recipe_Category_20 = new Recipe_Category();\r\nrecipe_Category_20.SubcategoryId = 111;\r\nvar subCategory_21 = new SubCategory();\r\nsubCategory_21.SubcategoryId = 0;\r\nsubCategory_21.Status = PublishedStatus.Arkiverad;\r\nsubCategory_21.State = 0;\r\nsubCategory_21.ShowImage = 0;\r\nsubCategory_21.ParentSubcategory = 0;\r\nsubCategory_21.Name = \"KeyHoleLabeled\";\r\nsubCategory_21.LatestChange = new DateTime(0);\r\nsubCategory_21.ImageId = 0;\r\nsubCategory_21.Id = 0;\r\nvar _category_22 = new Category();\r\nvar list_23 = new List<SubCategory>();\r\n_category_22.Subcategory = list_23;\r\n_category_22.Status = 0;\r\n_category_22.Name = \"KeyHoleLabeled\";\r\n_category_22.LatestChange = new DateTime(0);\r\n_category_22.Id = 111;\r\nSetValue(subCategory_21, \"_category\", _category_22);\r\nrecipe_Category_20.SubCategory = subCategory_21;\r\nrecipe_Category_20.RecipeId = 0;\r\nrecipe_Category_20.CategoryId = 8;\r\nlist_11.Add(recipe_Category_20);\r\nrecipe_0.Categories = list_11;\r\nrecipe_0.AverageRating = 0M;\r\nvar list_24 = new List<Recipe_Attribute>();\r\nrecipe_0.Attributes = list_24;", dump);
        }

        private static Recipe CreateFakeRecipe()
        {
            var recipe = new Recipe();

            const int recipeid = 1;

            recipe.Id = recipeid;
            recipe.Name = "Testreceptet";
            recipe.Portions = "4";
            var puffText = new TextGroup
            {
                RecipeId = recipe.Id,   
                TextTypeId = (int)TextTypes.Pufftext,
                Text = new Text { Body = "Pufftext", Header = "Header", Id = 111 }
            };

            var receptText = new TextGroup
            {
                RecipeId = recipe.Id,
                TextTypeId = (int)TextTypes.Recepttext,
                Text = new Text
                {
                    Body = "<ol>\r\n<li>Dela, k&auml;rna ur och strimla chilin. Skala och finhacka vitl&ouml;ken. Sk&auml;r fikonen i grova bitar.</li>\r\n<li>Putsa och sk&auml;r k&ouml;ttet i grytbitar. Strimla baconet.</li>\r\n<li>Bryn k&ouml;tt och bacon i omg&aring;ngar i olja i en stekpanna.</li>\r\n<li>Fr&auml;s tomatpur&eacute;, vitl&ouml;k, chilipulver och spiskummin i olja i en stor gryta/kastrull n&aring;gon minut. L&auml;gg i k&ouml;tt, bacon, strimlad chili, fikon, inlagd jalape&ntilde;o, torkad chilifrukt, fond, &ouml;l, vin, lagerblad, paprikapulver, oregano och koriander. Tills&auml;tt lite vatten om inte allt k&ouml;tt &auml;r t&auml;ckt.</li>\r\n<li>Koka upp och l&aring;t sm&aring;koka under lock tills k&ouml;ttet faller is&auml;r, det tar minst 3&ndash;4 timmar. R&ouml;r d&aring; och d&aring;, s&aring; att det inte br&auml;nner fast i bottnen. Smaka av med salt. Servera med tillbeh&ouml;ren inklusive <a href=\"http://www.ica.se/recept?recipeid=715431\">Godaste guacamolen</a>.</li>\r\n</ol>\r\n<h3>F&ouml;r alla</h3>\r\n<p>G&ouml;r glutenfri: v&auml;lj &ouml;l fritt fr&aring;n gluten eller med l&aring;g glutenhalt. (Se t ex lista fr&aring;n SLV.) Servera med l&auml;mpliga tillbeh&ouml;r.<br />G&ouml;r laktosfri: kontrollera inneh&aring;llet i fonden. Servera med l&auml;mpliga tillbeh&ouml;r.<br />G&ouml;r mj&ouml;lkproteinfri: kontrollera inneh&aring;llet i fonden. Servera med l&auml;mpliga tillbeh&ouml;r.<br />",
                    Header = "Header",
                    Id = 111
                }
            };


            var texts = new List<TextGroup>();
            texts.Add(puffText);
            texts.Add(receptText);

            recipe.Texts = texts;
            recipe.RecipeMedia = new RecipeMedia { ImageId = 1111 };
            recipe.Attributes = new List<Recipe_Attribute>();

            var cookingTime = new Recipe_Category
            {
                CategoryId = (int)CategoryTypes.CookingTime,
                SubCategory = new SubCategory(new Category
                {
                    Id = (int)SubCategoryTypes.Under30Min,
                    Name = SubCategoryTypes.Under30Min.ToString()
                })
            };
            cookingTime.SubCategory.Name = SubCategoryTypes.Under30Min.ToString();

            var difficultyLevel = new Recipe_Category
            {
                CategoryId = (int)CategoryTypes.Difficulty,
                SubCategory = new SubCategory(new Category
                {
                    Id = (int)SubCategoryTypes.MediumLevel,
                    Name = SubCategoryTypes.MediumLevel.ToString()
                })
            };
            difficultyLevel.SubCategory.Name = SubCategoryTypes.MediumLevel.ToString();

            var keyholeCategory = new Recipe_Category
            {
                CategoryId = (int)CategoryTypes.BitHealthier,
                SubCategory = new SubCategory(new Category { Id = (int)SubCategoryTypes.KeyHoleLabeled, Name = SubCategoryTypes.KeyHoleLabeled.ToString() })
            };
            keyholeCategory.SubCategory.Name = SubCategoryTypes.KeyHoleLabeled.ToString();
            keyholeCategory.SubcategoryId = (int)SubCategoryTypes.KeyHoleLabeled;

            recipe.Categories = new List<Recipe_Category> { cookingTime, difficultyLevel, keyholeCategory };

            recipe.Status = (int)Status.PUBL;

            var partOfRecipe = new PartOfRecipe();

            partOfRecipe.RecipeId = recipeid;
            partOfRecipe.Heading = "Del av recept";
            var ingredientsData = new List<IngredientData>();
            ingredientsData.Add(new IngredientData()
            {
                AlternativeDescription = "Alternativ beskrivning",
                Article = new Article() { AlternativeSpelling = "Tomat", Id = 2, Name = "Tomat", PluralName = "Tomaten" },
                Id = 2,
                ArticleId = 2,
                Comment = "Kommentar",
                CommentPrefix = "Prefixkommentar",
                IsPlural = false,
                MinQuantity = 0,
                Order = 1,
                Quantity = 1,
                Unit = new IngredientUnit() { Name = "Gurka", ShortName = "Gurk" }
            });
            partOfRecipe.IngredientDataRows = ingredientsData;

            recipe.RecipeParts = new[] { partOfRecipe };
            return recipe;
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

            var foos = new List<Foo> {new Foo {FooValue = "List foo 1"}, new Foo {FooValue = "List foo 2"}};
            
            var ifoos = new List<IFoo> {new Foo {FooValue = "List foo 1"}, new Foo {FooValue = "List foo 2"}};

            root.Foos = foos;
            
            root.Ifoos = ifoos;
            
            return root;
        }

        private static Root ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMember()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReference();

            var privateBar = new Bar {BarValue = "Private bar!! :)"};

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
            root.Foo2.Bar.BarFoo = new Foo{FooValue = "Root-Foo2-Bar-BarFoo"};
            SetValue(root.Foo, "PrivateFooProperty", "private foo property");
            SetValue(root.Foo, "_privateFooField", 999);
            return root;
        }
        
        private static Root ComplexRootWithPrivateMembersAndInterfaces()
        {
            var root = ComplexRootWithPrivateMembers();
            root.Foo = new Foo {FooValue = "IFoo!"};
            return root;
        }

        private static Root ComplexRootWithPrivateMembers()
        {
            var root = RootWithPrivateMembers();
            root.Foo2 = new Foo {FooValue = "Foo!", Bar = new Bar{BarValue = "Bar!"}};
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
