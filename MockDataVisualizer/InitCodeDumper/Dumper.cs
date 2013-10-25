using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public abstract class Dumper
    {
        internal readonly object Element;
        internal readonly Dumper Parent;
        
        private static Dictionary<int, string> _foundElements;
        internal static int ObjectCounter { get; set; }

        internal string ElementTypeName { get { return Element.GetType().Name; } }
        internal string ElementName { get; set; }
        
        internal Type Type { get { return Element.GetType(); } }
        internal string TypeName { get { return Type.Name; } }

        internal IEnumerable<MemberInfo> Members { get { return Element.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance); } }
        private IEnumerable<MemberInfo> PublicMembers { get { return Element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance); } }

        public abstract string GetPublicInitCode();
        public abstract string GetPrivateInitCode();

        public abstract string AddPublic(string initCode, string parentName, string elementNameInParent);
        public abstract string AddPrivate(string initCode, string parentName, string elementNameInParent);
        
        protected Dumper(Dumper parent, object element, string name)
        {
            Parent = parent;
            Element = element;
            ElementName = name;
        }

        public static string DumpInitilizationCodeMethod(object o)
        {
            if (o == null) return "First object can not be null.";

            var dumper = InitDumper(o);

            var initCode = dumper.GetPublicInitCode();

            var nameForMethod = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(dumper.ElementName);

            var completeCreateMethod = string.Format("public static {0} Create{1}(){{{2}{3}{4}return {5};{6}}}", dumper.ElementTypeName, nameForMethod, Environment.NewLine, initCode, Environment.NewLine, dumper.ElementName, Environment.NewLine);

            return completeCreateMethod;

        }

        public static string DumpInitlizationCode(object o)
        {
            if (o == null) return "First object can not be null.";

            var dumper = InitDumper(o);

            return string.Format("{0}", dumper.GetPublicInitCode());
        }

        private static Dumper InitDumper(object o)
        {
            _foundElements = new Dictionary<int, string>();

            ObjectCounter = 0;

            var name = ResolveTypeName(o.GetType()).ToLower();

            var dumper = GetDumper(null, o, name);

            return dumper;
        }

        internal bool IsElementAlreadyTouched(object element)
        {
            var hash = element.GetHashCode();
            
            return _foundElements.Any(t => _foundElements.ContainsKey(hash));
        }

        internal string GetNameOfAlreadyTouchedElement(object element)
        {
            var hash = element.GetHashCode();

            if (_foundElements.ContainsKey(hash))
            {
                return _foundElements[hash];
            }
            
            return null;
        }

        internal static Dumper GetDumper(Dumper parent, object o, string name)
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

        internal Type GetMemberType(MemberInfo member)
        {
            var fieldInfo = member as FieldInfo;
            var propertyInfo = member as PropertyInfo;

            if (fieldInfo == null && propertyInfo == null) return null;

            return fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
        }

        internal object GetMemberValue(MemberInfo member)
        {
            if (member == null) return null;

            if (member.Name.IndexOf("BackingField", StringComparison.Ordinal) != -1) return null;

            var fieldInfo = member as FieldInfo;
            var propertyInfo = member as PropertyInfo;
            
            if (fieldInfo == null && propertyInfo == null) return null;

            return fieldInfo != null ? fieldInfo.GetValue(Element) : propertyInfo.GetValue(Element, null);
        }

        internal void AddFoundElement(object element, string elementName)
        {
            var hash = element.GetHashCode();

            if (!_foundElements.ContainsKey(hash))
            {
                _foundElements.Add(hash, elementName);
            }
        }

        internal bool IsMemberPublic(MemberInfo member)
        {
            return PublicMembers.Contains(member);
        }

        internal bool CanWriteToMember(MemberInfo member)
        {
            var propertyInfo = member as PropertyInfo;
            
            if (propertyInfo != null)
            {
                return propertyInfo.CanWrite;
            }

            return member is FieldInfo;
        }

        internal bool CanWriteToMemberWithSetter(MemberInfo member)
        {
            var propertyInfo = member as PropertyInfo;

            if (propertyInfo != null && propertyInfo.GetSetMethod() != null)
            {
                return propertyInfo.CanWrite;
            }

            return member is FieldInfo;
        }

        public static string ResolveInitTypeName(Type type)
        {
            var initTypeName = ResolveTypeName(type);

            foreach (var argumentType in type.GetGenericArguments())
            {
                var argumentInitName = ResolveInitTypeName(argumentType);

                initTypeName = string.Format("{0}<{1}>", initTypeName, argumentInitName);
            }
            return initTypeName;
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