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
            var privateBar_1 = new Bar();
            privateBar_1.BarValue = "Private bar!! :)";
            SetValue(root_0, "PrivateBar", privateBar_1);
            var list_2 = new List<IFoo>();
            var foo_3 = new Foo();
            foo_3.FooValue = "List foo 1";
            SetValue(foo_3, "_privateFooField", 0);
            list_2.Add(foo_3);
            var foo_4 = new Foo();
            foo_4.FooValue = "List foo 2";
            SetValue(foo_4, "_privateFooField", 0);
            list_2.Add(foo_4);
            SetValue(root_0, "FooList", list_2);
            SetValue(root_0, "PrivateProperty", "The private property");
            root_0.RootId = 1;
            root_0.RootName = "The Root!";
            var foo_5 = new Foo();
            SetValue(foo_5, "PrivateFooProperty", "private foo property");
            foo_5.FooValue = "IFoo!";
            SetValue(foo_5, "_privateFooField", 999);
            root_0.Foo = foo_5;
            var foo2_6 = new Foo();
            foo2_6.FooValue = "Foo!";
            var bar_7 = new Bar();
            bar_7.BarValue = "Bar!";
            var barFoo_8 = new Foo();
            barFoo_8.FooValue = "Root-Foo2-Bar-BarFoo";
            SetValue(barFoo_8, "_privateFooField", 0);
            bar_7.BarFoo = barFoo_8;
            foo2_6.Bar = bar_7;
            foo2_6.Root = root_0;
            SetValue(foo2_6, "_privateFooField", 0);
            root_0.Foo2 = foo2_6;
            var list_9 = new List<Foo>();
            var foo_10 = new Foo();
            foo_10.FooValue = "List foo 1";
            SetValue(foo_10, "_privateFooField", 0);
            list_9.Add(foo_10);
            var foo_11 = new Foo();
            foo_11.FooValue = "List foo 2";
            SetValue(foo_11, "_privateFooField", 0);
            list_9.Add(foo_11);
            root_0.Foos = list_9;
            var list_12 = new List<IFoo>();
            var foo_13 = new Foo();
            foo_13.FooValue = "List foo 1";
            SetValue(foo_13, "_privateFooField", 0);
            list_12.Add(foo_13);
            var foo_14 = new Foo();
            foo_14.FooValue = "List foo 2";
            SetValue(foo_14, "_privateFooField", 0);
            list_12.Add(foo_14);
            root_0.Ifoos = list_12;
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