using System;
using System.Collections;
using System.Collections.Generic;
using MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers;
using MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers;
using System.Linq;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public abstract class DumperBase
    {
        internal static readonly string SetValueMethod = "public static void SetValue<T>(object obj, string propName, T val) { if (obj == null) throw new ArgumentNullException(\"obj\"); var t = obj.GetType(); if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) != null) { t.InvokeMember(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, obj, new object[] { val }); } else { FieldInfo fi = null; while (fi == null && t != null) { fi = t.GetField(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance); t = t.BaseType; } if (fi == null) throw new ArgumentOutOfRangeException(\"propName\", string.Format(\"Field {0} was not found in Type {1}\", propName, obj.GetType().FullName)); fi.SetValue(obj, val); } }";
        internal readonly object Element;
        internal readonly DumperBase Parent;
        
        internal string ElementName { get; set; }

        internal static List<Type> SkipTypes;
        internal static Dictionary<int, string> _foundElements;
        internal static int ObjectCounter { get; set; }
        internal static bool DumpPublicOnly { get; set; }

        protected DumperBase(DumperBase parent, object element, string name)
        {
            Parent = parent;
            Element = element;
            ElementName = name;
        }

        static DumperBase()
        {
            SkipTypes = CfgLoader.GetHardCodedSkipTypes();    
        }

        internal abstract void ResolveInitCode(CodeBuilder codeBuilder);
        
        public static string DumpCode(object o, DumpMode mode, Visibility visibility)
        {
            if (o == null) return "First object can not be null.";

            if (ShouldSkipType(o)) return "In skip types list.";

            if (visibility == Visibility.PublicOnly)
            {
                DumpPublicOnly = true;
            }

            _foundElements = new Dictionary<int, string>();

            ObjectCounter = 0;

            var dumper = GetDumper(null, o, ResolveTypeName(o.GetType()).ToLower());

            var codeBuilder = new CodeBuilder();

            dumper.ResolveInitCode(codeBuilder);

            var code = codeBuilder.ToString();

            if (mode == DumpMode.WrappedCode || mode == DumpMode.WrappedCodeAndMethod)
            {
                code = string.Format("public static {0} Create{1}(){{{2}{3}{4}return {5};{6}}}", dumper.Element.GetType().Name, dumper.ElementName, Environment.NewLine, code, Environment.NewLine, dumper.ElementName, Environment.NewLine);
            }

            if (mode == DumpMode.WrappedCodeAndMethod)
            {
                code = string.Format("{0}{1}{2}", code, Environment.NewLine, SetValueMethod);
            }

            return code;
        }

        internal static bool ShouldSkipType(object o)
        {
            return SkipTypes.Any(skipType => skipType == o.GetType());
        }

        internal static DumperBase GetDumper(DumperBase parent, object o, string name)
        {
            var type = o.GetType();

            if (o is Guid) return new GuidTypeDumper(parent, o, name);

            if (o is DateTime) return new DateTimeTypeDumper(parent, o, name);

            if (o is Enum) return new EnumTypeDumper(parent, o, name);

            if (type.IsValueType && !type.IsEnum && !type.IsPrimitive && type != typeof(decimal)) return new ObjectTypeDumper(parent, o, name);

            if (o is ValueType) return new ValueTypeDumper(parent, o, name);

            if (o is string) return new StringTypeDumper(parent, o, name);

            if (o is Array) return new ArrayTypeDumper(parent, o , name);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>)) return new DictionaryTypeDumper(parent, o, name);

            if (o is IEnumerable) return new EnumerableTypeDumper(parent, o, name);

            return new ObjectTypeDumper(parent, o, name);
        }

        internal static string ResolveTypeName(Type type)
        {
            return IsGenericType(type) ? type.Name.Substring(0, type.Name.IndexOf('`')) : type.Name;
        }

        internal static bool IsGenericType(Type type)
        {
            return type.GetGenericArguments().Length != 0;
        }
    }

    public enum DumpMode
    {
        CodeOnly = 0,
        WrappedCode = 1,
        WrappedCodeAndMethod = 2,
    }

    public enum Visibility
    {
        PrivateAndPublic = 0,
        PublicOnly = 1
    }
}