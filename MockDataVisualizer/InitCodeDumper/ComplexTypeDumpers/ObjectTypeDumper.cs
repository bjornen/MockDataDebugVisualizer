using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers
{
    public class ObjectTypeDumper : AbstractComplexTypeDumper
    {
        internal ObjectTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name)
        {
            var typeName = name;

            if (IsGenericType(element.GetType()))
            {
                typeName = ResolveTypeName(element.GetType());
            }

            ElementName = string.Format("{0}_{1}", typeName, ObjectCounter++);

            AddFoundElement(element, ElementName);
        }

        public override void ResolveTypeInitilization(CodeBuilder codeBuilder)
        {
            var typeName = ResolveInitTypeName(Element.GetType());

            if (Element.GetType().GetConstructor(Type.EmptyTypes) == null)
            {
                var initCode = string.Format("var {0} = ({1}) FormatterServices.GetUninitializedObject(typeof ({1}));", ElementName, typeName);
                codeBuilder.AddCode(initCode);
            }
            else
            {
                var initCode = string.Format("var {0} = new {1}();", ElementName, typeName);
                codeBuilder.AddCode(initCode);
            }
        }

        public override void ResolveMembers(CodeBuilder codeBuilder)
        {
            foreach (var member in Members)
            {
                var memberValue = GetMemberValue(member);

                if (memberValue == null) continue;

                if (IsElementAlreadyTouched(memberValue))
                {
                    if (IsMemberPublic(member) && CanWriteToMemberWithSetter(member))
                    {
                        var memberInitCode = string.Format("{0}.{1} = {2};", ElementName, member.Name, GetNameOfAlreadyTouchedElement(memberValue));

                        codeBuilder.AddCode(memberInitCode);
                    }
                    else if (CanWriteToMember(member))
                    {
                        var memberInitCode = string.Format("SetValue({0}, \"{1}\", {2});", ElementName, member.Name, GetNameOfAlreadyTouchedElement(memberValue));

                        codeBuilder.AddCode(memberInitCode);
                    }
                }
                else
                {
                    if (IsMemberPublic(member) && CanWriteToMemberWithSetter(member))
                    {
                        var dumper = GetDumper(this, memberValue, member.Name);

                        dumper.AddPublicMember(codeBuilder);

                        string value;

                        if (dumper is AbstractOneLineInitDumper)
                        {
                            value = codeBuilder.PopCode();
                        }
                        else
                        {
                            value = dumper.ElementName;
                        }

                        var initCode = string.Format("{0}.{1} = {2};", ElementName, member.Name, value);

                        codeBuilder.AddCode(initCode);
                    }
                    else if (CanWriteToMember(member))
                    {
                        var dumper = GetDumper(this, memberValue, member.Name);

                        dumper.AddPublicMember(codeBuilder);

                        string value;

                        if (dumper is AbstractOneLineInitDumper)
                        {
                            value = codeBuilder.PopCode();
                        }
                        else
                        {
                            value = dumper.ElementName;
                        }

                        var line = string.Format("SetValue({0}, \"{1}\", {2});", ElementName, member.Name, value);

                        codeBuilder.AddCode(line);
                    }
                }
            }
        }






        internal IEnumerable<MemberInfo> Members { get { return Element.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance); } } // use .OrderBy(x => x.Name); to make unit tests work
        private IEnumerable<MemberInfo> PublicMembers { get { return Element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance); } }


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
    }
}
