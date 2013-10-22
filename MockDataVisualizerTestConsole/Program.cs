using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using MockDataDebugVisualizer;
using MockDataVisualizerTestConsole.Tests;
using MockDataVisualizerTestConsole.Tests.TestObjects;
using MockDataVisualizerTestConsole.Tests.TestObjects.Customer;

namespace MockDataVisualizerTestConsole
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Test();

            //var root = ObjectCreateCodeDumperTests.ComplexRootWithPrivateMembersAndInterfacesWithPrivateMembersAndCircularReferenceAndPrivateObjectMemberAndSingleArgumentEnumerableAndPrivateEnumerable();

            //var wr = new WeakReference(root);

            //MockDataVisualizer.TestShowVisualizer(wr);
        }

        public static void Test()
        {

        }

        public static void SetValue<T>(object obj, string propName, T val)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var t = obj.GetType();

            if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) != null)
            {
                t.InvokeMember(propName,
                               BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty |
                               BindingFlags.Instance, null, obj, new object[] {val});
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
                                                          string.Format("Field {0} was not found in Type {1}", propName,
                                                                        obj.GetType().FullName));
                fi.SetValue(obj, val);
            }
        }
    }
}