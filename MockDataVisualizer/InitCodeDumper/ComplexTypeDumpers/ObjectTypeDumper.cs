﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers
{
    public class ObjectTypeDumper : AbstractComplexTypeDumper
    {
        private IEnumerable<MemberInfo> Members { get { return Element.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance); } } // use .OrderBy(x => x.Name); to make unit tests work
        private IEnumerable<MemberInfo> PublicMembers { get { return Element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance); } }

        internal ObjectTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name)
        {
            AddFoundElement(element, ElementName);
        }

        public override void ResolveTypeInitilization(CodeBuilder codeBuilder)
        {
            var typeName = ResolveInitTypeName(Element.GetType());

            if (Element.GetType().GetConstructor(Type.EmptyTypes) == null)
            {
                codeBuilder.AddCode(string.Format("var {0} = ({1}) FormatterServices.GetUninitializedObject(typeof ({1}));", ElementName, typeName));
            }
            else
            {
                codeBuilder.AddCode(string.Format("var {0} = new {1}();", ElementName, typeName));
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
                        codeBuilder.AddCode(string.Format("{0}.{1} = {2};", ElementName, member.Name, GetNameOfAlreadyTouchedElement(memberValue)));
                    }
                    else if (CanWriteToMember(member))
                    {
                        codeBuilder.AddCode(string.Format("SetValue({0}, \"{1}\", {2});", ElementName, member.Name, GetNameOfAlreadyTouchedElement(memberValue)));
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

                        codeBuilder.AddCode(string.Format("{0}.{1} = {2};", ElementName, member.Name, value));
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

                        codeBuilder.AddCode(string.Format("SetValue({0}, \"{1}\", {2});", ElementName, member.Name, value));
                    }
                }
            }
        }

        private static bool IsElementAlreadyTouched(object element)
        {
            var hash = element.GetHashCode();

            return _foundElements.Any(t => _foundElements.ContainsKey(hash));
        }

        private static string GetNameOfAlreadyTouchedElement(object element)
        {
            var hash = element.GetHashCode();

            if (_foundElements.ContainsKey(hash))
            {
                return _foundElements[hash];
            }

            return null;
        }

        private object GetMemberValue(MemberInfo member)
        {
            if (member == null) return null;

            if (member.Name.IndexOf("BackingField", StringComparison.Ordinal) != -1) return null;

            var fieldInfo = member as FieldInfo;
            var propertyInfo = member as PropertyInfo;

            if (fieldInfo == null && propertyInfo == null) return null;

            return fieldInfo != null ? fieldInfo.GetValue(Element) : propertyInfo.GetValue(Element, null);
        }

        private static void AddFoundElement(object element, string elementName)
        {
            var hash = element.GetHashCode();

            if (!_foundElements.ContainsKey(hash))
            {
                _foundElements.Add(hash, elementName);
            }
        }

        private bool IsMemberPublic(MemberInfo member)
        {
            return PublicMembers.Contains(member);
        }

        private static bool CanWriteToMember(MemberInfo member)
        {
            var propertyInfo = member as PropertyInfo;

            if (propertyInfo != null)
            {
                return propertyInfo.CanWrite;
            }

            return member is FieldInfo;
        }

        private static bool CanWriteToMemberWithSetter(MemberInfo member)
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
