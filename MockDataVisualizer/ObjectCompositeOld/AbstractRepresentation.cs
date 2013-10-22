using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace MockDataVisualizer.ObjectComposite
{
    public abstract class AbstractRepresentation
    {
        public readonly object Element;
        
        public static readonly List<int> HashListOfFoundElements;

        public static int ObjectCounter { get { return _objectCounter++; } }
        private static int _objectCounter;

        public string ElementName { get; set; }

        public IEnumerable<MemberInfo> Members { get { return Element.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly); } }
        private IEnumerable<MemberInfo> PublicMembers { get { return Element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly); } }

        static AbstractRepresentation()
        {
            HashListOfFoundElements = new List<int>();
            _objectCounter = 0;
        }

        protected AbstractRepresentation(object element, string elementName)
        {
            Element = element;
            HashListOfFoundElements.Add(element.GetHashCode());
            ElementName = elementName;
        }

        public abstract string CreateElementInitilizationString(string elementParentExcludingThis, string elementNameFromParent, object parentElement);

        public bool AlreadyTouched(object value)
        {
            var hash = value.GetHashCode();
            for (var i = 0; i < HashListOfFoundElements.Count; i++)
            {
                if (HashListOfFoundElements[i] == hash)
                    return true;
            }
            return false;
        }

        public AbstractRepresentation GetRepresentation(object o, string name)
        {
            if (o is ValueType || o is string) return new ValueTyeRepresentation(o, name);

            if (o is IEnumerable) return new EnumerableRepresentation(o, name);
            
            return new ObjectRepresentation(o, name);
        }

        public Type GetMemberType(MemberInfo member)
        {
            var fieldInfo = member as FieldInfo;
            var propertyInfo = member as PropertyInfo;

            if (fieldInfo == null && propertyInfo == null) return null;

            return fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
        }

        public object GetMemberValue(MemberInfo member)
        {
            if (member == null) return null;

            if (member.Name.IndexOf("BackingField", StringComparison.Ordinal) != -1) return null;

            var fieldInfo = member as FieldInfo;
            var propertyInfo = member as PropertyInfo;
            
            if (fieldInfo == null && propertyInfo == null) return null;

            //var m = Element.GetType().GetMember(member.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            return fieldInfo != null ? fieldInfo.GetValue(Element) : propertyInfo.GetValue(Element, null);
        }
        protected bool IsMemberPublic(MemberInfo member)
        {
            return PublicMembers.Contains(member);
        }

        protected bool CanWriteToMember(MemberInfo member)
        {
            var propertyInfo = member as PropertyInfo;
            
            if (propertyInfo != null)
            {
                return propertyInfo.CanWrite;
            }
            return false;
        }
    }
}
