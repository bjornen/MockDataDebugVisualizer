using System;
using System.Collections.Generic;
using System.Linq;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public abstract class DumperBase
    {
        internal static readonly string SetValueMethod = "public static void SetValue<T>(object obj, string propName, T val) { if (obj == null) throw new ArgumentNullException(\"obj\"); var t = obj.GetType(); if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) != null) { t.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, obj, new object[] { val }); } else { FieldInfo fi = null; while (fi == null && t != null) { fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance); t = t.BaseType; } if (fi == null) throw new ArgumentOutOfRangeException(\"propName\", string.Format(\"Field {0} was not found in Type {1}\", propName, obj.GetType().FullName)); fi.SetValue(obj, val); } }";
        internal readonly object Element;
        
        internal string ElementName { get; set; }

        internal static List<Type> SkipTypes;
        internal static Dictionary<int, string> _foundElements;
        internal static int ObjectCounter { get; set; }
        internal static bool DumpPublicOnly { get; set; }

        protected DumperBase(object element, string name)
        {
            Element = element;
            ElementName = name;
        }

        static DumperBase()
        {
            SkipTypes = CfgLoader.GetHardCodedSkipTypes();    
        }

        public abstract void ResolveInitCode(CodeBuilder codeBuilder);
        
        public static string DumpCode(object o, DumpMode mode, Visibility visibility)
        {
            if (o == null) return "First object can not be null.";

            if (ShouldSkipType(o)) return "In skip types list.";

            ResetDumper(visibility);

            var dumper = DumperFactory.GetDumper(o, ResolveActualTypeName(o.GetType()).ToLower());

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            var code = codeBuilder.ToString();

            if (mode == DumpMode.WrappedCode || mode == DumpMode.WrappedCodeAndMethod)
            {
                code = $"public static {dumper.Element.GetType().Name} Create{dumper.ElementName}(){{{Environment.NewLine}{code}{Environment.NewLine}return {dumper.ElementName};{Environment.NewLine}}}";
            }

            if (mode == DumpMode.WrappedCodeAndMethod)
            {
                code = $"{code}{Environment.NewLine}{SetValueMethod}";
            }

            return code;
        }

        public static void ResetDumper(Visibility visibility)
        {
            if (visibility == Visibility.PublicOnly)
            {
                DumpPublicOnly = true;
            }
            if (visibility == Visibility.PrivateAndPublic)
            {
                DumpPublicOnly = false;
            }

            _foundElements = new Dictionary<int, string>();

            ObjectCounter = 0;
        }

        internal static bool ShouldSkipType(object o)
        {
            return SkipTypes.Any(skipType => skipType == o.GetType());
        }

        internal static string ResolveActualTypeName(Type type)
        {
            var trueType = type;

            if (type.BaseType != null && type.Namespace == "System.Data.Entity.DynamicProxies")
            {
                trueType = type.BaseType;
            }

            return IsGenericType(trueType) ? trueType.Name.Substring(0, trueType.Name.IndexOf('`')) : trueType.Name;
        }

        internal static bool IsGenericType(Type type)
        {
            return type.GetGenericArguments().Length != 0;
        }
    }
}