using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers
{
    public class ObjectType : AbstractComplexType
    {
        private IEnumerable<MemberInfo> Members => Element.GetType().GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).OrderByDescending(x => x.Name);
        private IEnumerable<MemberInfo> PublicMembers => Element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);

        internal ObjectType(object element, string name) : base(element, name) { }

        public override void ResolveTypeInitilization(CodeBuilder codeBuilder)
        {
            var typeName = ResolveInitTypeName(ElementType);

            if (Element.GetType().GetConstructor(Type.EmptyTypes) == null && !Element.GetType().IsValueType) //No public constructors or not struct
            {
                var initCode = $"var {ElementName} = ({typeName}) FormatterServices.GetUninitializedObject(typeof ({typeName}));";

                codeBuilder.AddCode(initCode);
            }
            else
            {
                var initCode = $"var {ElementName} = new {typeName}();";

                codeBuilder.AddCode(initCode);
            }
        }

        protected Type ElementType => Element.GetType();

        public override void ResolveMembers(CodeBuilder codeBuilder)
        {
            if (Members == null) return;

            foreach (var member in Members)
            {
                try
                {
                    var memberValue = GetMemberValue(member);

                    if (!IsDumpable(memberValue)) continue;

                    var dumper = DumperFactory.GetDumper(memberValue, member.Name);

                    dumper.ResolveInitCode(codeBuilder);

                    WriteMemberInitCode(codeBuilder, member);
                }

                catch(Exception){} //Dump the rest of the members anyway
            }
        }

        private void WriteMemberInitCode(CodeBuilder codeBuilder, MemberInfo member)
        {
            if (IsPublicMember(member))
            {
                codeBuilder.AddCode($"{ElementName}.{member.Name} = {codeBuilder.PopInitValue()};");
            }
            else if (CanWriteToMember(member) && !DumpPublicOnly)
            {
                codeBuilder.AddCode($"SetValue({ElementName}, \"{member.Name}\", {codeBuilder.PopInitValue()});");
            }
        }

        private bool IsPublicMember(MemberInfo member)
        {
            return PublicMembers.Contains(member) && CanWriteToMemberWithSetter(member);
        }

        private static bool IsDumpable(object obj)
        {
            if (obj == null) return false;

            if (ShouldSkipType(obj)) return false;

            return obj.GetType().IsVisible;
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

            if (propertyInfo?.GetSetMethod() != null)
            {
                return propertyInfo.CanWrite;
            }

            return member is FieldInfo;
        }

        public static string ResolveInitTypeName(Type type)
        {
            var initTypeName = ResolveActualTypeName(type);

            foreach (var argumentType in type.GetGenericArguments())
            {
                var argumentInitName = ResolveInitTypeName(argumentType);

                initTypeName = $"{initTypeName}<{argumentInitName}>";
            }

            return initTypeName;
        }
    }
}
