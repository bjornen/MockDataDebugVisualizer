using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using MockDataDebugVisualizer.InitCodeDumper;
using MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers;
using MockDataVisualizerTestConsole.Tests.TestObjects;
using MockDataVisualizerTestConsole.Tests.TestObjects.Customer;
using Xunit;

namespace MockDataVisualizerTestConsole.Tests
{
    public class ObjectCreateCodeDumperTests
    {

        [Fact]
        public void Can_dump_object()
        {
            var root = Root();

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";", dump);
        }

        [Fact]
        public void Can_dump_object_with_private_members()
        {
            var root = RootWithPrivateMembers();

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nSetValue(root_0, \"_backing\", \"The private backing field\");\r\nSetValue(root_0, \"_privateField\", \"The private field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members()
        {
            var root = ComplexRootWithPrivateMembers();

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nvar Foo2_1 = new Foo();\r\nFoo2_1.FooValue = \"Foo!\";\r\nvar Bar_2 = new Bar();\r\nBar_2.BarValue = \"Bar!\";\r\nFoo2_1.Bar = Bar_2;\r\nSetValue(Foo2_1, \"_privateFooField\", 0);\r\nroot_0.Foo2 = Foo2_1;\r\nSetValue(root_0, \"_backing\", \"The private backing field\");\r\nSetValue(root_0, \"_privateField\", \"The private field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces()
        {
            var root = ComplexRootWithPrivateMembersAndInterfaces();

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nvar Foo_1 = new Foo();\r\nFoo_1.FooValue = \"IFoo!\";\r\nSetValue(Foo_1, \"_privateFooField\", 0);\r\nroot_0.Foo = Foo_1;\r\nvar Foo2_2 = new Foo();\r\nFoo2_2.FooValue = \"Foo!\";\r\nvar Bar_3 = new Bar();\r\nBar_3.BarValue = \"Bar!\";\r\nFoo2_2.Bar = Bar_3;\r\nSetValue(Foo2_2, \"_privateFooField\", 0);\r\nroot_0.Foo2 = Foo2_2;\r\nSetValue(root_0, \"_backing\", \"The private backing field\");\r\nSetValue(root_0, \"_privateField\", \"The private field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembers();

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nvar Foo_1 = new Foo();\r\nSetValue(Foo_1, \"PrivateFooProperty\", \"private foo property\");\r\nFoo_1.FooValue = \"IFoo!\";\r\nSetValue(Foo_1, \"_privateFooField\", 999);\r\nroot_0.Foo = Foo_1;\r\nvar Foo2_2 = new Foo();\r\nFoo2_2.FooValue = \"Foo!\";\r\nvar Bar_3 = new Bar();\r\nBar_3.BarValue = \"Bar!\";\r\nvar BarFoo_4 = new Foo();\r\nBarFoo_4.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(BarFoo_4, \"_privateFooField\", 0);\r\nBar_3.BarFoo = BarFoo_4;\r\nFoo2_2.Bar = Bar_3;\r\nSetValue(Foo2_2, \"_privateFooField\", 0);\r\nroot_0.Foo2 = Foo2_2;\r\nSetValue(root_0, \"_backing\", \"The private backing field\");\r\nSetValue(root_0, \"_privateField\", \"The private field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReference();

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nvar Foo_1 = new Foo();\r\nSetValue(Foo_1, \"PrivateFooProperty\", \"private foo property\");\r\nFoo_1.FooValue = \"IFoo!\";\r\nSetValue(Foo_1, \"_privateFooField\", 999);\r\nroot_0.Foo = Foo_1;\r\nvar Foo2_2 = new Foo();\r\nFoo2_2.FooValue = \"Foo!\";\r\nvar Bar_3 = new Bar();\r\nBar_3.BarValue = \"Bar!\";\r\nvar BarFoo_4 = new Foo();\r\nBarFoo_4.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(BarFoo_4, \"_privateFooField\", 0);\r\nBar_3.BarFoo = BarFoo_4;\r\nFoo2_2.Bar = Bar_3;\r\nFoo2_2.Root = root_0;\r\nSetValue(Foo2_2, \"_privateFooField\", 0);\r\nroot_0.Foo2 = Foo2_2;\r\nSetValue(root_0, \"_backing\", \"The private backing field\");\r\nSetValue(root_0, \"_privateField\", \"The private field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference_and_private_object_member()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMember();

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nvar Foo_1 = new Foo();\r\nSetValue(Foo_1, \"PrivateFooProperty\", \"private foo property\");\r\nFoo_1.FooValue = \"IFoo!\";\r\nSetValue(Foo_1, \"_privateFooField\", 999);\r\nroot_0.Foo = Foo_1;\r\nvar Foo2_2 = new Foo();\r\nFoo2_2.FooValue = \"Foo!\";\r\nvar Bar_3 = new Bar();\r\nBar_3.BarValue = \"Bar!\";\r\nvar BarFoo_4 = new Foo();\r\nBarFoo_4.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(BarFoo_4, \"_privateFooField\", 0);\r\nBar_3.BarFoo = BarFoo_4;\r\nFoo2_2.Bar = Bar_3;\r\nFoo2_2.Root = root_0;\r\nSetValue(Foo2_2, \"_privateFooField\", 0);\r\nroot_0.Foo2 = Foo2_2;\r\nvar PrivateBar_5 = new Bar();\r\nPrivateBar_5.BarValue = \"Private bar!! :)\";\r\nSetValue(root_0, \"PrivateBar\", PrivateBar_5);\r\nSetValue(root_0, \"_backing\", \"The private backing field\");\r\nSetValue(root_0, \"_privateField\", \"The private field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference_and_private_object_member_and_single_argument_enumerable()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMemberAndSingleArgumentEnumerable();

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nvar Foo_1 = new Foo();\r\nSetValue(Foo_1, \"PrivateFooProperty\", \"private foo property\");\r\nFoo_1.FooValue = \"IFoo!\";\r\nSetValue(Foo_1, \"_privateFooField\", 999);\r\nroot_0.Foo = Foo_1;\r\nvar Foo2_2 = new Foo();\r\nFoo2_2.FooValue = \"Foo!\";\r\nvar Bar_3 = new Bar();\r\nBar_3.BarValue = \"Bar!\";\r\nvar BarFoo_4 = new Foo();\r\nBarFoo_4.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(BarFoo_4, \"_privateFooField\", 0);\r\nBar_3.BarFoo = BarFoo_4;\r\nFoo2_2.Bar = Bar_3;\r\nFoo2_2.Root = root_0;\r\nSetValue(Foo2_2, \"_privateFooField\", 0);\r\nroot_0.Foo2 = Foo2_2;\r\nvar PrivateBar_5 = new Bar();\r\nPrivateBar_5.BarValue = \"Private bar!! :)\";\r\nSetValue(root_0, \"PrivateBar\", PrivateBar_5);\r\nvar Foos_6 = new List<Foo>();\r\nvar Foo_7 = new Foo();\r\nFoo_7.FooValue = \"List foo 1\";\r\nSetValue(Foo_7, \"_privateFooField\", 0);\r\nFoos_6.Add(Foo_7);\r\nvar Foo_8 = new Foo();\r\nFoo_8.FooValue = \"List foo 2\";\r\nSetValue(Foo_8, \"_privateFooField\", 0);\r\nFoos_6.Add(Foo_8);\r\nroot_0.Foos = Foos_6;\r\nvar Ifoos_9 = new List<IFoo>();\r\nvar Foo_10 = new Foo();\r\nFoo_10.FooValue = \"List foo 1\";\r\nSetValue(Foo_10, \"_privateFooField\", 0);\r\nIfoos_9.Add(Foo_10);\r\nvar Foo_11 = new Foo();\r\nFoo_11.FooValue = \"List foo 2\";\r\nSetValue(Foo_11, \"_privateFooField\", 0);\r\nIfoos_9.Add(Foo_11);\r\nroot_0.Ifoos = Ifoos_9;\r\nSetValue(root_0, \"_backing\", \"The private backing field\");\r\nSetValue(root_0, \"_privateField\", \"The private field\");", dump);
        }

        [Fact]
        public void Can_dump_complex_object_with_private_members_and_interfaces_with_private_members_and_circular_reference_and_private_object_member_and_single_argument_enumerable_and_private_enumerable()
        {
            var root = ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMemberAndSingleArgumentEnumerableAndPrivateEnumerable();

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nSetValue(root_0, \"PrivateProperty\", \"The private property\");\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nvar Foo_1 = new Foo();\r\nSetValue(Foo_1, \"PrivateFooProperty\", \"private foo property\");\r\nFoo_1.FooValue = \"IFoo!\";\r\nSetValue(Foo_1, \"_privateFooField\", 999);\r\nroot_0.Foo = Foo_1;\r\nvar Foo2_2 = new Foo();\r\nFoo2_2.FooValue = \"Foo!\";\r\nvar Bar_3 = new Bar();\r\nBar_3.BarValue = \"Bar!\";\r\nvar BarFoo_4 = new Foo();\r\nBarFoo_4.FooValue = \"Root-Foo2-Bar-BarFoo\";\r\nSetValue(BarFoo_4, \"_privateFooField\", 0);\r\nBar_3.BarFoo = BarFoo_4;\r\nFoo2_2.Bar = Bar_3;\r\nFoo2_2.Root = root_0;\r\nSetValue(Foo2_2, \"_privateFooField\", 0);\r\nroot_0.Foo2 = Foo2_2;\r\nvar PrivateBar_5 = new Bar();\r\nPrivateBar_5.BarValue = \"Private bar!! :)\";\r\nSetValue(root_0, \"PrivateBar\", PrivateBar_5);\r\nvar Foos_6 = new List<Foo>();\r\nvar Foo_7 = new Foo();\r\nFoo_7.FooValue = \"List foo 1\";\r\nSetValue(Foo_7, \"_privateFooField\", 0);\r\nFoos_6.Add(Foo_7);\r\nvar Foo_8 = new Foo();\r\nFoo_8.FooValue = \"List foo 2\";\r\nSetValue(Foo_8, \"_privateFooField\", 0);\r\nFoos_6.Add(Foo_8);\r\nroot_0.Foos = Foos_6;\r\nvar Ifoos_9 = new List<IFoo>();\r\nvar Foo_10 = new Foo();\r\nFoo_10.FooValue = \"List foo 1\";\r\nSetValue(Foo_10, \"_privateFooField\", 0);\r\nIfoos_9.Add(Foo_10);\r\nvar Foo_11 = new Foo();\r\nFoo_11.FooValue = \"List foo 2\";\r\nSetValue(Foo_11, \"_privateFooField\", 0);\r\nIfoos_9.Add(Foo_11);\r\nroot_0.Ifoos = Ifoos_9;\r\nvar FooList_12 = new List<IFoo>();\r\nvar Foo_13 = new Foo();\r\nFoo_13.FooValue = \"List foo 1\";\r\nSetValue(Foo_13, \"_privateFooField\", 0);\r\nFooList_12.Add(Foo_13);\r\nvar Foo_14 = new Foo();\r\nFoo_14.FooValue = \"List foo 2\";\r\nSetValue(Foo_14, \"_privateFooField\", 0);\r\nFooList_12.Add(Foo_14);\r\nSetValue(root_0, \"FooList\", FooList_12);\r\nvar IfooList_15 = new List<Foo>();\r\nvar Foo_16 = new Foo();\r\nFoo_16.FooValue = \"List foo 1\";\r\nSetValue(Foo_16, \"_privateFooField\", 0);\r\nIfooList_15.Add(Foo_16);\r\nvar Foo_17 = new Foo();\r\nFoo_17.FooValue = \"List foo 2\";\r\nSetValue(Foo_17, \"_privateFooField\", 0);\r\nIfooList_15.Add(Foo_17);\r\nroot_0.IfooList = IfooList_15;\r\nSetValue(root_0, \"_backing\", \"The private backing field\");\r\nSetValue(root_0, \"_privateField\", \"The private field\");", dump);
        }        
        
        [Fact]
        public void Can_create_complete_create_method()
        {
            var root = Root();

            var dump = DumperBase.DumpInitilizationCodeMethod(root);

            Assert.Equal("public static Root CreateRoot_0(){\r\nvar root_0 = new Root();\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nreturn root_0;\r\n}", dump);
        }

        [Fact]
        public void Can_dump_value_type_lists()
        {
            var root = Root();

            root.Numbers = new List<int>{1,2,3,4,5,6,7};

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nvar Numbers_1 = new List<Int32>();\r\nNumbers_1.Add(1);\r\nNumbers_1.Add(2);\r\nNumbers_1.Add(3);\r\nNumbers_1.Add(4);\r\nNumbers_1.Add(5);\r\nNumbers_1.Add(6);\r\nNumbers_1.Add(7);\r\nroot_0.Numbers = Numbers_1;", dump);
        }

        [Fact]
        public void Can_dump_Array()
        {
            var root = Root();

            root.FooArray = new[] {new Foo {FooValue = "1"}, new Foo {FooValue = "2"}, new Foo {FooValue = "3"},};

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nvar FooArray_1 = new Foo[3];\r\nvar Foo_2 = new Foo();\r\nFoo_2.FooValue = \"1\";\r\nSetValue(Foo_2, \"_privateFooField\", 0);\r\nFooArray_1[0] = Foo_2;\r\nvar Foo_3 = new Foo();\r\nFoo_3.FooValue = \"2\";\r\nSetValue(Foo_3, \"_privateFooField\", 0);\r\nFooArray_1[1] = Foo_3;\r\nvar Foo_4 = new Foo();\r\nFoo_4.FooValue = \"3\";\r\nSetValue(Foo_4, \"_privateFooField\", 0);\r\nFooArray_1[2] = Foo_4;\r\nroot_0.FooArray = FooArray_1;", dump);
        }

        [Fact]
        public void Can_dump_value_type_array()
        {
            var root = Root();

            root.NumbersArray = new[] { 11, 22, 33 };

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootId = 1;\r\nroot_0.RootName = \"The Root!\";\r\nvar NumbersArray_1 = new Int32[3];\r\nNumbersArray_1[0] = 11;\r\nNumbersArray_1[1] = 22;\r\nNumbersArray_1[2] = 33;\r\nroot_0.NumbersArray = NumbersArray_1;", dump);
        }

        [Fact]
        public void Can_dump_guid()
        {
            var root = new StructObj();

            root.Unique = Guid.Parse("5182c68d-3459-4bfa-9d14-a3548eb14da8");

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var structobj_0 = new StructObj();\r\nstructobj_0.Unique = Guid.Parse(\"5182c68d-3459-4bfa-9d14-a3548eb14da8\");\r\nstructobj_0.Date = new DateTime(0);", dump);
        }

        [Fact]
        public void Can_dump_DateTime()
        {
            var root = new StructObj();

            root.Unique = Guid.Parse("5182c68d-3459-4bfa-9d14-a3548eb14da8");

            root.Date = new DateTime(635169253125431379);

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var structobj_0 = new StructObj();\r\nstructobj_0.Unique = Guid.Parse(\"5182c68d-3459-4bfa-9d14-a3548eb14da8\");\r\nstructobj_0.Date = new DateTime(635169253125431379);", dump);
        }

        [Fact]
        public void Can_dump_serviced_entity()
        {
            var svcObj = new ServiceObject();

            var servicedEntity = new ServicedEntity<IEnumerable<Foo>>(new List<Foo> {new Foo {FooValue = "foo"}, new Foo {FooValue = "bar"}});

            SetValue(svcObj, "_storesUsed", servicedEntity);

            var dump = DumperBase.DumpInitlizationCode(servicedEntity);

            Assert.Equal("var ServicedEntity_0 = new ServicedEntity<IEnumerable<Foo>>();\r\nSetValue(ServicedEntity_0, \"IsValid\", true);\r\nvar Entity_1 = new List<Foo>();\r\nvar Foo_2 = new Foo();\r\nFoo_2.FooValue = \"foo\";\r\nSetValue(Foo_2, \"_privateFooField\", 0);\r\nEntity_1.Add(Foo_2);\r\nvar Foo_3 = new Foo();\r\nFoo_3.FooValue = \"bar\";\r\nSetValue(Foo_3, \"_privateFooField\", 0);\r\nEntity_1.Add(Foo_3);\r\nSetValue(ServicedEntity_0, \"Entity\", Entity_1);", dump);
        }

        [Fact]
        public void Can_dump_boolean()
        {
            var bo = new BoolObject {TheBool = true};

            var dump = DumperBase.DumpInitlizationCode(bo);

            Assert.Equal("var boolobject_0 = new BoolObject();\r\nboolobject_0.TheBool = true;", dump);
        }

        [Fact]
        public void Can_dump_Customer()
        {
            var masterCustomer = GetCustomer();

            var dump = DumperBase.DumpInitlizationCode(masterCustomer);

            Assert.Equal("var mastercustomer_0 = new MasterCustomer();\r\nmastercustomer_0.Id = 200000030;\r\nmastercustomer_0.CardId = 128524964M;\r\nmastercustomer_0.StudentInd = \"N\";\r\nmastercustomer_0.StudyEndDate = new DateTime(0);\r\nmastercustomer_0.UpdateStudyEndDate = false;\r\nmastercustomer_0.StudyEndDateSpecified = false;\r\nmastercustomer_0.StudyFocusAreaDesc = \"\";\r\nmastercustomer_0.CivicRegistrationNumber = 192222222222M;\r\nmastercustomer_0.Address = \"KASERNVÄGEN LAGERKRANTSPLAT\";\r\nmastercustomer_0.BestBonusAccount = 521579177M;\r\nvar Household_1 = new Household();\r\nvar ServicedEntity_2 = new ServicedEntity<IEnumerable<StoreUsedStats>>();\r\nSetValue(ServicedEntity_2, \"IsValid\", true);\r\nvar Entity_3 = new StoreUsedStats[9];\r\nvar StoreUsedStats_4 = new StoreUsedStats();\r\nStoreUsedStats_4.MyStoresCode = MyStoresCode.CustomerSelection;\r\nSetValue(StoreUsedStats_4, \"StoreId\", 1510);\r\nSetValue(StoreUsedStats_4, \"NumberOfPurchases\", 0);\r\nSetValue(StoreUsedStats_4, \"Points\", 0);\r\nEntity_3[0] = StoreUsedStats_4;\r\nvar StoreUsedStats_5 = new StoreUsedStats();\r\nSetValue(StoreUsedStats_5, \"StoreId\", 13716);\r\nSetValue(StoreUsedStats_5, \"NumberOfPurchases\", 40);\r\nSetValue(StoreUsedStats_5, \"Points\", 1000);\r\nStoreUsedStats_5.MyStoresCode = MyStoresCode.MonthlyBatchjob;\r\nEntity_3[1] = StoreUsedStats_5;\r\nvar StoreUsedStats_6 = new StoreUsedStats();\r\nSetValue(StoreUsedStats_6, \"StoreId\", 13662);\r\nSetValue(StoreUsedStats_6, \"NumberOfPurchases\", 30);\r\nSetValue(StoreUsedStats_6, \"Points\", 500);\r\nStoreUsedStats_6.MyStoresCode = MyStoresCode.MonthlyBatchjob;\r\nEntity_3[2] = StoreUsedStats_6;\r\nvar StoreUsedStats_7 = new StoreUsedStats();\r\nSetValue(StoreUsedStats_7, \"StoreId\", 1292);\r\nSetValue(StoreUsedStats_7, \"NumberOfPurchases\", 25);\r\nSetValue(StoreUsedStats_7, \"Points\", 300);\r\nStoreUsedStats_7.MyStoresCode = MyStoresCode.MonthlyBatchjob;\r\nEntity_3[3] = StoreUsedStats_7;\r\nvar StoreUsedStats_8 = new StoreUsedStats();\r\nSetValue(StoreUsedStats_8, \"StoreId\", 2773);\r\nSetValue(StoreUsedStats_8, \"NumberOfPurchases\", 25);\r\nSetValue(StoreUsedStats_8, \"Points\", 3226);\r\nStoreUsedStats_8.MyStoresCode = MyStoresCode.MonthlyBatchjob;\r\nEntity_3[4] = StoreUsedStats_8;\r\nvar StoreUsedStats_9 = new StoreUsedStats();\r\nSetValue(StoreUsedStats_9, \"StoreId\", 3519);\r\nSetValue(StoreUsedStats_9, \"NumberOfPurchases\", 6);\r\nSetValue(StoreUsedStats_9, \"Points\", 934);\r\nStoreUsedStats_9.MyStoresCode = MyStoresCode.MonthlyBatchjob;\r\nEntity_3[5] = StoreUsedStats_9;\r\nvar StoreUsedStats_10 = new StoreUsedStats();\r\nSetValue(StoreUsedStats_10, \"StoreId\", 2771);\r\nSetValue(StoreUsedStats_10, \"NumberOfPurchases\", 3);\r\nSetValue(StoreUsedStats_10, \"Points\", 526);\r\nStoreUsedStats_10.MyStoresCode = MyStoresCode.MonthlyBatchjob;\r\nEntity_3[6] = StoreUsedStats_10;\r\nvar StoreUsedStats_11 = new StoreUsedStats();\r\nSetValue(StoreUsedStats_11, \"StoreId\", 2780);\r\nSetValue(StoreUsedStats_11, \"NumberOfPurchases\", 3);\r\nSetValue(StoreUsedStats_11, \"Points\", 124);\r\nStoreUsedStats_11.MyStoresCode = MyStoresCode.MonthlyBatchjob;\r\nEntity_3[7] = StoreUsedStats_11;\r\nvar StoreUsedStats_12 = new StoreUsedStats();\r\nSetValue(StoreUsedStats_12, \"StoreId\", 8927);\r\nSetValue(StoreUsedStats_12, \"NumberOfPurchases\", 2);\r\nSetValue(StoreUsedStats_12, \"Points\", 336);\r\nStoreUsedStats_12.MyStoresCode = MyStoresCode.MonthlyBatchjob;\r\nEntity_3[8] = StoreUsedStats_12;\r\nSetValue(ServicedEntity_2, \"Entity\", Entity_3);\r\nSetValue(Household_1, \"_storesUsed\", ServicedEntity_2);\r\nSetValue(Household_1, \"_parentMasterCustomer\", mastercustomer_0);\r\nSetValue(mastercustomer_0, \"Household\", Household_1);\r\nmastercustomer_0.CellPhoneNumber = \"\";\r\nmastercustomer_0.City = \"BORÅS\";\r\nmastercustomer_0.CoAttName = \"ANNELI GLENNLÖV\";\r\nmastercustomer_0.Country = \"SE\";\r\nmastercustomer_0.CustomerStatus = \"\";\r\nmastercustomer_0.CustomerSubscriptionChoice = 100M;\r\nmastercustomer_0.HomeNumber = \"\";\r\nmastercustomer_0.Forename = \"ICA\";\r\nmastercustomer_0.NameDepartment = \"\";\r\nmastercustomer_0.ProtectedAddressInd = \"\";\r\nmastercustomer_0.Surname = \"ICANDER\";\r\nmastercustomer_0.ZipCode = 50482M;\r\nvar EmailAdresses_13 = new List<Email>();\r\nvar Email_14 = new Email();\r\nEmail_14.Address = \"ylva.persson@ica.se\";\r\nEmail_14.Type = \"01\";\r\nEmailAdresses_13.Add(Email_14);\r\nvar Email_15 = new Email();\r\nEmail_15.Address = \"ica.icander@ica.se\";\r\nEmail_15.Type = \"02\";\r\nEmailAdresses_13.Add(Email_15);\r\nmastercustomer_0.EmailAdresses = EmailAdresses_13;\r\nmastercustomer_0.Fullname = \"Ica Icander\";\r\nvar Phones_16 = new List<Phone>();\r\nSetValue(mastercustomer_0, \"Phones\", Phones_16);\r\nvar ServicedEntity_17 = new ServicedEntity<BonusData>();\r\nSetValue(ServicedEntity_17, \"IsValid\", true);\r\nvar Entity_18 = new BonusData();\r\nEntity_18.CurrentAmountOfPointsOfAccount = 0M;\r\nEntity_18.DiscountAmountPerYear = 112.35M;\r\nEntity_18.AmountOfSwedishCrownsToNextBonusCheck = 2500M;\r\nEntity_18.PurchasesMadeOnIcaStatiolCurrentYear = 10972.05M;\r\nEntity_18.RecievedBonusOfCurrentYear = 125.00M;\r\nSetValue(Entity_18, \"_amountOfSwedishCrowndsToNextBonusCheck\", 2500M);\r\nSetValue(ServicedEntity_17, \"Entity\", Entity_18);\r\nSetValue(mastercustomer_0, \"_bonus\", ServicedEntity_17);\r\nvar ServicedEntity_19 = new ServicedEntity<AccountData>();\r\nSetValue(ServicedEntity_19, \"IsValid\", false);\r\nvar Exception_20 = new NullReferenceException();\r\nSetValue(Exception_20, \"HResult\", -2147467261);\r\nSetValue(Exception_20, \"_message\", \"Object reference not set to an instance of an object.\");\r\nSetValue(Exception_20, \"_HResult\", -2147467261);\r\nSetValue(ServicedEntity_19, \"Exception\", Exception_20);\r\nSetValue(mastercustomer_0, \"_account\", ServicedEntity_19);\r\nvar ServicedEntity_21 = new ServicedEntity<CardData>();\r\nSetValue(ServicedEntity_21, \"IsValid\", false);\r\nvar Exception_22 = new NullReferenceException();\r\nSetValue(Exception_22, \"HResult\", -2147467261);\r\nSetValue(Exception_22, \"_message\", \"Object reference not set to an instance of an object.\");\r\nSetValue(Exception_22, \"_HResult\", -2147467261);\r\nSetValue(ServicedEntity_21, \"Exception\", Exception_22);\r\nSetValue(mastercustomer_0, \"_card\", ServicedEntity_21);", dump);
        }

        [Fact]
        public void Can_dump_customer_household()
        {
            var customer = new MasterCustomer();
            
            var hh = new Household(customer);
            
            SetValue(customer, "Household", hh);
            
            customer.BestBonusAccount = 33M;
            
            var se = new ServicedEntity<IEnumerable<StoreUsedStats>>(new List<StoreUsedStats>{new StoreUsedStats(123, 20, MyStoresCode.MonthlyBatchjob)});
            
            SetValue(hh, "_storesUsed", se);

            var dump = DumperBase.DumpInitlizationCode(customer);

            Assert.Equal("var mastercustomer_0 = new MasterCustomer();\r\nmastercustomer_0.Id = 0;\r\nmastercustomer_0.StudyEndDate = new DateTime(0);\r\nmastercustomer_0.UpdateStudyEndDate = false;\r\nmastercustomer_0.StudyEndDateSpecified = false;\r\nmastercustomer_0.CivicRegistrationNumber = 0M;\r\nmastercustomer_0.BestBonusAccount = 33M;\r\nvar Household_1 = new Household();\r\nvar ServicedEntity_2 = new ServicedEntity<IEnumerable<StoreUsedStats>>();\r\nSetValue(ServicedEntity_2, \"IsValid\", true);\r\nvar Entity_3 = new List<StoreUsedStats>();\r\nvar StoreUsedStats_4 = new StoreUsedStats();\r\nStoreUsedStats_4.MyStoresCode = MyStoresCode.MonthlyBatchjob;\r\nSetValue(StoreUsedStats_4, \"StoreId\", 123);\r\nSetValue(StoreUsedStats_4, \"NumberOfPurchases\", 20);\r\nSetValue(StoreUsedStats_4, \"Points\", 0);\r\nEntity_3.Add(StoreUsedStats_4);\r\nSetValue(ServicedEntity_2, \"Entity\", Entity_3);\r\nSetValue(Household_1, \"_storesUsed\", ServicedEntity_2);\r\nSetValue(Household_1, \"_parentMasterCustomer\", mastercustomer_0);\r\nSetValue(mastercustomer_0, \"Household\", Household_1);\r\nmastercustomer_0.CellPhoneNumber = \"\";\r\nmastercustomer_0.CustomerSubscriptionChoice = 0M;\r\nmastercustomer_0.HomeNumber = \"\";\r\nmastercustomer_0.ZipCode = 0M;", dump);
        }

        [Fact]
        public void Can_dump_protected_set()
        {
            var root = new Root();
            
            root.InitilizeProtected();

            var dump = DumperBase.DumpInitlizationCode(root);

            Assert.Equal("var root_0 = new Root();\r\nroot_0.RootId = 0;\r\nvar ProtectedSetFoo_1 = new Foo();\r\nProtectedSetFoo_1.FooValue = \"Protected Foo\";\r\nSetValue(ProtectedSetFoo_1, \"_privateFooField\", 0);\r\nSetValue(root_0, \"ProtectedSetFoo\", ProtectedSetFoo_1);", dump);
        }

        [Fact]
        public void Can_resolve_init_type_name_for_serviced_entity()
        {
            var se = new ServicedEntity<IEnumerable<StoreUsedStats>>(new List<StoreUsedStats> { new StoreUsedStats(123, 20, MyStoresCode.MonthlyBatchjob) });

            var dumper = ObjectTypeDumper.ResolveInitTypeName(se.GetType());

            Assert.Equal("ServicedEntity<IEnumerable<StoreUsedStats>>", dumper);
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

            var dump = DumperBase.DumpInitlizationCode(enumObject);

            Assert.Equal("var enumobject_0 = new EnumObject();\r\nenumobject_0.Colors1 = Colors.blue;\r\nSetValue(enumobject_0, \"ColorsWithNumbers\", ColorsWithNumbers.red);", dump);
        }

        [Fact]
        public void Can_dump_non_public_ctor()
        {
            var obj = (PrivateCtorObject)Activator.CreateInstance(typeof(PrivateCtorObject), true);
            obj.Value = "The Value";

            var dump = DumperBase.DumpInitlizationCode(obj);

            Assert.Equal("var privatectorobject_0 = (PrivateCtorObject) FormatterServices.GetUninitializedObject(typeof (PrivateCtorObject));\r\nprivatectorobject_0.Value = \"The Value\";", dump);
        }

        [Fact]
        public void Can_dump_non_parameterless_ctor()
        {
            var obj = new NonParameterlessCtorObj("The values!");

            var dump = DumperBase.DumpInitlizationCode(obj);

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

            var dump = DumperBase.DumpInitlizationCode(list);

            Assert.Equal("var list_0 = new List<Foo>();\r\nvar Foo_1 = new Foo();\r\nFoo_1.FooValue = \"1\";\r\nSetValue(Foo_1, \"_privateFooField\", 0);\r\nlist_0.Add(Foo_1);\r\nvar Foo_2 = new Foo();\r\nFoo_2.FooValue = \"2\";\r\nSetValue(Foo_2, \"_privateFooField\", 0);\r\nlist_0.Add(Foo_2);", dump);
        }

        [Fact]
        public void Can_dump_string_string_dictionary()
        {
            var dic = new Dictionary<string, string> {{"key", "value"}};
            
            var dump = DumperBase.DumpInitlizationCode(dic);

              Assert.Equal("var dictionary_0 = new Dictionary<String, String>();\r\ndictionary_0.Add(\"key\", \"value\");", dump);
        }

        [Fact]
        public void Can_dump_string_object_dictionary()
        {
            var dic = new Dictionary<string, Foo> { { "key", new Foo{FooValue = "foo" }} };

            var dump = DumperBase.DumpInitlizationCode(dic);

            Assert.Equal("var dictionary_0 = new Dictionary<String, Foo>();\r\nvar Foo_2_3 = new Foo();\r\nFoo_2_3.FooValue = \"foo\";\r\nSetValue(Foo_2_3, \"_privateFooField\", 0);\r\ndictionary_0.Add(\"key\", Foo_2_3);", dump);
        }

        [Fact]
        public void Can_dump_object_object_dictionary()
        {
            var dic = new Dictionary<Foo, Foo> { { new Foo { FooValue = "fookey" }, new Foo { FooValue = "foovalue" } } };

            var dump = DumperBase.DumpInitlizationCode(dic);

            Assert.Equal("var dictionary_0 = new Dictionary<Foo, Foo>();\r\nvar Foo_1_3 = new Foo();\r\nFoo_1_3.FooValue = \"fookey\";\r\nSetValue(Foo_1_3, \"_privateFooField\", 0);\r\nvar Foo_2_4 = new Foo();\r\nFoo_2_4.FooValue = \"foovalue\";\r\nSetValue(Foo_2_4, \"_privateFooField\", 0);\r\ndictionary_0.Add(Foo_1_3, Foo_2_4);", dump);
        }

        [Fact]
        public void Can_dump_object_string_dictionary()
        {
            var dic = new Dictionary<Foo, string> { { new Foo { FooValue = "foo" }, "key" } };

            var dump = DumperBase.DumpInitlizationCode(dic);

            Assert.Equal("var dictionary_0 = new Dictionary<Foo, String>();\r\nvar Foo_1_3 = new Foo();\r\nFoo_1_3.FooValue = \"foo\";\r\nSetValue(Foo_1_3, \"_privateFooField\", 0);\r\ndictionary_0.Add(Foo_1_3, \"key\");", dump);
        }


        public static MasterCustomer GetCustomer()
        {
            var mastercustomer_0 = new MasterCustomer();
            mastercustomer_0.Id = 200000030;
            mastercustomer_0.CardId = 128524964M;
            mastercustomer_0.StudentInd = "N";
            mastercustomer_0.StudyEndDate = new DateTime(0);
            mastercustomer_0.UpdateStudyEndDate = false;
            mastercustomer_0.StudyEndDateSpecified = false;
            mastercustomer_0.StudyFocusAreaDesc = "";
            mastercustomer_0.CivicRegistrationNumber = 192222222222M;
            mastercustomer_0.Address = "KASERNVÄGEN LAGERKRANTSPLAT";
            mastercustomer_0.BestBonusAccount = 521579177M;
            var Household_1 = (Household)FormatterServices.GetUninitializedObject(typeof(Household));
            var ServicedEntity_2 = (ServicedEntity<IEnumerable<StoreUsedStats>>)FormatterServices.GetUninitializedObject(typeof(ServicedEntity<IEnumerable<StoreUsedStats>>));
            SetValue(ServicedEntity_2, "IsValid", true);


            var Entity_3 = new StoreUsedStats[9];
            var StoreUsedStats_4 = (StoreUsedStats)FormatterServices.GetUninitializedObject(typeof(StoreUsedStats));
            StoreUsedStats_4.MyStoresCode = MyStoresCode.CustomerSelection;
            SetValue(StoreUsedStats_4, "StoreId", 1510);
            SetValue(StoreUsedStats_4, "NumberOfPurchases", 0);
            SetValue(StoreUsedStats_4, "Points", 0);
            Entity_3[0] = StoreUsedStats_4;
            var StoreUsedStats_5 = (StoreUsedStats)FormatterServices.GetUninitializedObject(typeof(StoreUsedStats));
            StoreUsedStats_5.MyStoresCode = MyStoresCode.MonthlyBatchjob;
            SetValue(StoreUsedStats_5, "StoreId", 13716);
            SetValue(StoreUsedStats_5, "NumberOfPurchases", 40);
            SetValue(StoreUsedStats_5, "Points", 1000);
            Entity_3[1] = StoreUsedStats_5;
            var StoreUsedStats_6 = (StoreUsedStats)FormatterServices.GetUninitializedObject(typeof(StoreUsedStats));
            StoreUsedStats_6.MyStoresCode = MyStoresCode.MonthlyBatchjob;
            SetValue(StoreUsedStats_6, "StoreId", 13662);
            SetValue(StoreUsedStats_6, "NumberOfPurchases", 30);
            SetValue(StoreUsedStats_6, "Points", 500);
            Entity_3[2] = StoreUsedStats_6;
            var StoreUsedStats_7 = (StoreUsedStats)FormatterServices.GetUninitializedObject(typeof(StoreUsedStats));
            StoreUsedStats_7.MyStoresCode = MyStoresCode.MonthlyBatchjob;
            SetValue(StoreUsedStats_7, "StoreId", 1292);
            SetValue(StoreUsedStats_7, "NumberOfPurchases", 25);
            SetValue(StoreUsedStats_7, "Points", 300);
            Entity_3[3] = StoreUsedStats_7;
            var StoreUsedStats_8 = (StoreUsedStats)FormatterServices.GetUninitializedObject(typeof(StoreUsedStats));
            StoreUsedStats_8.MyStoresCode = MyStoresCode.MonthlyBatchjob;
            SetValue(StoreUsedStats_8, "StoreId", 2773);
            SetValue(StoreUsedStats_8, "NumberOfPurchases", 25);
            SetValue(StoreUsedStats_8, "Points", 3226);
            Entity_3[4] = StoreUsedStats_8;
            var StoreUsedStats_9 = (StoreUsedStats)FormatterServices.GetUninitializedObject(typeof(StoreUsedStats));
            StoreUsedStats_9.MyStoresCode = MyStoresCode.MonthlyBatchjob;
            SetValue(StoreUsedStats_9, "StoreId", 3519);
            SetValue(StoreUsedStats_9, "NumberOfPurchases", 6);
            SetValue(StoreUsedStats_9, "Points", 934);
            Entity_3[5] = StoreUsedStats_9;
            var StoreUsedStats_10 = (StoreUsedStats)FormatterServices.GetUninitializedObject(typeof(StoreUsedStats));
            StoreUsedStats_10.MyStoresCode = MyStoresCode.MonthlyBatchjob;
            SetValue(StoreUsedStats_10, "StoreId", 2771);
            SetValue(StoreUsedStats_10, "NumberOfPurchases", 3);
            SetValue(StoreUsedStats_10, "Points", 526);
            Entity_3[6] = StoreUsedStats_10;
            var StoreUsedStats_11 = (StoreUsedStats)FormatterServices.GetUninitializedObject(typeof(StoreUsedStats));
            StoreUsedStats_11.MyStoresCode = MyStoresCode.MonthlyBatchjob;
            SetValue(StoreUsedStats_11, "StoreId", 2780);
            SetValue(StoreUsedStats_11, "NumberOfPurchases", 3);
            SetValue(StoreUsedStats_11, "Points", 124);
            Entity_3[7] = StoreUsedStats_11;
            var StoreUsedStats_12 = (StoreUsedStats)FormatterServices.GetUninitializedObject(typeof(StoreUsedStats));
            StoreUsedStats_12.MyStoresCode = MyStoresCode.MonthlyBatchjob;
            SetValue(StoreUsedStats_12, "StoreId", 8927);
            SetValue(StoreUsedStats_12, "NumberOfPurchases", 2);
            SetValue(StoreUsedStats_12, "Points", 336);
            Entity_3[8] = StoreUsedStats_12;
            SetValue(ServicedEntity_2, "Entity", Entity_3);
            SetValue(Household_1, "_storesUsed", ServicedEntity_2);
            SetValue(Household_1, "_parentMasterCustomer", mastercustomer_0);
            SetValue(mastercustomer_0, "Household", Household_1);
            mastercustomer_0.CellPhoneNumber = "";
            mastercustomer_0.City = "BORÅS";
            mastercustomer_0.CoAttName = "ANNELI GLENNLÖV";
            mastercustomer_0.Country = "SE";
            mastercustomer_0.CustomerStatus = "";
            mastercustomer_0.CustomerSubscriptionChoice = 100M;
            mastercustomer_0.HomeNumber = "";
            mastercustomer_0.Forename = "ICA";
            mastercustomer_0.NameDepartment = "";
            mastercustomer_0.ProtectedAddressInd = "";
            mastercustomer_0.Surname = "ICANDER";
            mastercustomer_0.ZipCode = 50482M;

            var EmailAdresses_13 = new List<Email>();
            var Email_14 = (Email)FormatterServices.GetUninitializedObject(typeof(Email));
            Email_14.Address = "ylva.persson@ica.se";
            Email_14.Type = "01";
            EmailAdresses_13.Add(Email_14);
            var Email_15 = (Email)FormatterServices.GetUninitializedObject(typeof(Email));
            Email_15.Address = "ica.icander@ica.se";
            Email_15.Type = "02";
            EmailAdresses_13.Add(Email_15);
            mastercustomer_0.EmailAdresses = EmailAdresses_13;
            mastercustomer_0.Fullname = "Ica Icander";

            var Phones_16 = new List<Phone>();
            SetValue(mastercustomer_0, "Phones", Phones_16);
            var ServicedEntity_17 = (ServicedEntity<BonusData>)FormatterServices.GetUninitializedObject(typeof(ServicedEntity<BonusData>));
            SetValue(ServicedEntity_17, "IsValid", true);
            var Entity_18 = new BonusData();
            Entity_18.CurrentAmountOfPointsOfAccount = 0M;
            Entity_18.DiscountAmountPerYear = 112.35M;
            Entity_18.AmountOfSwedishCrownsToNextBonusCheck = 2500M;
            Entity_18.PurchasesMadeOnIcaStatiolCurrentYear = 10972.05M;
            Entity_18.RecievedBonusOfCurrentYear = 125.00M;
            SetValue(Entity_18, "_amountOfSwedishCrowndsToNextBonusCheck", 2500M);
            SetValue(ServicedEntity_17, "Entity", Entity_18);
            SetValue(mastercustomer_0, "_bonus", ServicedEntity_17);
            var ServicedEntity_19 = (ServicedEntity<AccountData>)FormatterServices.GetUninitializedObject(typeof(ServicedEntity<AccountData>));
            SetValue(ServicedEntity_19, "IsValid", false);
            var Exception_20 = new NullReferenceException();
            SetValue(Exception_20, "HResult", -2147467261);
            SetValue(Exception_20, "_message", "Object reference not set to an instance of an object.");
            SetValue(Exception_20, "_HResult", -2147467261);
            SetValue(ServicedEntity_19, "Exception", Exception_20);
            SetValue(mastercustomer_0, "_account", ServicedEntity_19);
            var ServicedEntity_21 = (ServicedEntity<CardData>)FormatterServices.GetUninitializedObject(typeof(ServicedEntity<CardData>));
            SetValue(ServicedEntity_21, "IsValid", false);
            var Exception_22 = new NullReferenceException();
            SetValue(Exception_22, "HResult", -2147467261);
            SetValue(Exception_22, "_message", "Object reference not set to an instance of an object.");
            SetValue(Exception_22, "_HResult", -2147467261);
            SetValue(ServicedEntity_21, "Exception", Exception_22);
            SetValue(mastercustomer_0, "_card", ServicedEntity_21);

            return mastercustomer_0;
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
