using System;
using System.Collections.Generic;
using System.Reflection;
using MockDataDebugVisualizer.DebugVisualizers.InitCode;
using MockDataDebugVisualizer.DebugVisualizers.InitCodeMethod;
using MockDataDebugVisualizer.DebugVisualizers.InitCodeMethodSetValue;
using MockDataVisualizerTestConsole.Tests;
using MockDataVisualizerTestConsole.Tests.TestObjects;

namespace MockDataVisualizerTestConsole
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Root createroot0 = Createroot_0();

            Root root =
                ObjectCreateCodeDumperTests
                    .ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMemberAndSingleArgumentEnumerableAndPrivateEnumerable
                    ();

            var wr = new WeakReference(root);

            InitCodeVisualizer.TestShowVisualizer(wr);
            InitCodeMethodVisualizer.TestShowVisualizer(wr);
            InitCodeMethodAndSetValueVisualizer.TestShowVisualizer(wr);
        }

        public static Root Createroot_0()
        {
            var root_0 = new Root();
            SetValue(root_0, "PrivateProperty", "The private property");
            root_0.RootId = 1;
            root_0.RootName = "The Root!";
            var foo_1 = new Foo();
            SetValue(foo_1, "PrivateFooProperty", "private foo property");
            foo_1.FooValue = "IFoo!";
            SetValue(foo_1, "_privateFooField", 999);
            root_0.Foo = foo_1;
            var foo2_2 = new Foo();
            foo2_2.FooValue = "Foo!";
            var bar_3 = new Bar();
            bar_3.BarValue = "Bar!";
            var barFoo_4 = new Foo();
            barFoo_4.FooValue = "Root-Foo2-Bar-BarFoo";
            SetValue(barFoo_4, "_privateFooField", 0);
            bar_3.BarFoo = barFoo_4;
            foo2_2.Bar = bar_3;
            foo2_2.Root = root_0;
            SetValue(foo2_2, "_privateFooField", 0);
            root_0.Foo2 = foo2_2;
            var privateBar_5 = new Bar();
            privateBar_5.BarValue = "Private bar!! :)";
            SetValue(root_0, "PrivateBar", privateBar_5);
            var list_6 = new List<Foo>();
            var foo_7 = new Foo();
            foo_7.FooValue = "List foo 1";
            SetValue(foo_7, "_privateFooField", 0);
            list_6.Add(foo_7);
            var foo_8 = new Foo();
            foo_8.FooValue = "List foo 2";
            SetValue(foo_8, "_privateFooField", 0);
            list_6.Add(foo_8);
            root_0.Foos = list_6;
            var list_9 = new List<IFoo>();
            var foo_10 = new Foo();
            foo_10.FooValue = "List foo 1";
            SetValue(foo_10, "_privateFooField", 0);
            list_9.Add(foo_10);
            var foo_11 = new Foo();
            foo_11.FooValue = "List foo 2";
            SetValue(foo_11, "_privateFooField", 0);
            list_9.Add(foo_11);
            root_0.Ifoos = list_9;
            var list_12 = new List<IFoo>();
            var foo_13 = new Foo();
            foo_13.FooValue = "List foo 1";
            SetValue(foo_13, "_privateFooField", 0);
            list_12.Add(foo_13);
            var foo_14 = new Foo();
            foo_14.FooValue = "List foo 2";
            SetValue(foo_14, "_privateFooField", 0);
            list_12.Add(foo_14);
            SetValue(root_0, "FooList", list_12);
            var list_15 = new List<Foo>();
            var foo_16 = new Foo();
            foo_16.FooValue = "List foo 1";
            SetValue(foo_16, "_privateFooField", 0);
            list_15.Add(foo_16);
            var foo_17 = new Foo();
            foo_17.FooValue = "List foo 2";
            SetValue(foo_17, "_privateFooField", 0);
            list_15.Add(foo_17);
            root_0.IfooList = list_15;
            SetValue(root_0, "_backing", "The private backing field");
            SetValue(root_0, "_privateField", "The private field");
            return root_0;
        }

        public static void SetValue<T>(object obj, string propName, T val)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            Type t = obj.GetType();
            if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) != null)
            {
                t.InvokeMember(propName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
                    null, obj, new object[] {val});
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
                    throw new ArgumentOutOfRangeException("propName",
                        string.Format("Field {0} was not found in Type {1}", propName, obj.GetType().FullName));
                fi.SetValue(obj, val);
            }
        }
    }
}