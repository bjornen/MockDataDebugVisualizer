using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using MockDataDebugVisualizer.InitCodeDumper.Dumpers;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public abstract class DumperBase
    {
        internal readonly object Element;
        internal readonly DumperBase Parent;
        internal string ElementName { get; set; }

        internal static Dictionary<int, string> _foundElements;
        internal static int ObjectCounter { get; set; }

        protected DumperBase(DumperBase parent, object element, string name)
        {
            Parent = parent;
            Element = element;
            ElementName = name;
        }

        internal abstract void AddPublicMember(CodeBuilder codeBuilder);
        internal abstract void AddPrivateMember(CodeBuilder codeBuilder);
        
        public static string DumpInitilizationCodeMethod(object o)
        {
            if (o == null) return "First object can not be null.";

            var dumper = InitDumper(o);

            var codeBuilder = new CodeBuilder();

            var code = string.Empty;

            var objectDumper = dumper as AbstractComplexTypeDumper;

            if (objectDumper != null)
            {
                objectDumper.AddPublicMember(codeBuilder);

                code = codeBuilder.ToString();
            }

            var oneLineDumper = dumper as IOneLineInitDumper;

            if (oneLineDumper != null)
            {
                code = oneLineDumper.PublicOneLineInitCode();
            }

            var nameForMethod = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(dumper.ElementName);

            var completeCreateMethod = string.Format("public static {0} Create{1}(){{{2}{3}{4}return {5};{6}}}", dumper.Element.GetType().Name, nameForMethod, Environment.NewLine, code, Environment.NewLine, dumper.ElementName, Environment.NewLine);

            return completeCreateMethod;
        }

        public static string DumpInitlizationCode(object o)
        {
            if (o == null) return "First object can not be null.";

            var dumper = InitDumper(o);

            var codeBuilder = new CodeBuilder();

            var code = string.Empty;

            var objectDumper = dumper as AbstractComplexTypeDumper;

            if (objectDumper != null)
            {
                objectDumper.AddPublicMember(codeBuilder);

                code = codeBuilder.ToString();
            }

            var oneLineDumper = dumper as IOneLineInitDumper;

            if (oneLineDumper != null)
            {
                code = oneLineDumper.PublicOneLineInitCode();
            }

            return string.Format("{0}", code);
        }

        private static DumperBase InitDumper(object o)
        {
            _foundElements = new Dictionary<int, string>();

            ObjectCounter = 0;

            var name = ResolveTypeName(o.GetType()).ToLower();

            var dumper = GetDumper(null, o, name);

            return dumper;
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
}