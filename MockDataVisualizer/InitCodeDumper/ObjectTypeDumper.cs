using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class ObjectTypeDumper : Dumper
    {
        internal ObjectTypeDumper(Dumper parent, object element, string name) : base(parent, element, name)
        {
            var typeName = name;

            if (IsGenericType(element.GetType()))
            {
                typeName = ResolveTypeName(element.GetType());
            }

            ElementName = string.Format("{0}_{1}", typeName, ObjectCounter++);

            AddFoundElement(element, ElementName);
        }

        public override void GetPublicInitCode(CodeBuilder codeBuilder)
        {
            //string initCode;
            var typeName = ResolveInitTypeName(Element.GetType());

            if (Type.GetConstructor(Type.EmptyTypes) == null)
            {
                var initCode = string.Format("var {0} = ({1}) FormatterServices.GetUninitializedObject(typeof ({1}));", ElementName, typeName);
                codeBuilder.AddCode(initCode);
            }
            else
            {
                var initCode = string.Format("var {0} = new {1}();", ElementName, typeName);    
                codeBuilder.AddCode(initCode);
            }
            
            foreach (var member in Members)
            {
                var memberValue = GetMemberValue(member);

                if (memberValue == null) continue;

                //var memberInitCode = string.Empty;

                var memberBuilder = new CodeBuilder();

                if (IsElementAlreadyTouched(memberValue))
                {
                    if (IsMemberPublic(member) && CanWriteToMemberWithSetter(member))
                    {
                        var memberInitCode = string.Format("{0}.{1} = {2};", ElementName, member.Name, GetNameOfAlreadyTouchedElement(memberValue));
                        memberBuilder.AddCode(memberInitCode);
                    }
                    else if (CanWriteToMember(member))
                    {
                        var memberInitCode = string.Format("SetValue({0}, \"{1}\", {2});", ElementName, member.Name, GetNameOfAlreadyTouchedElement(memberValue));
                        memberBuilder.AddCode(memberInitCode);
                    }
                }
                else
                {
                    if (IsMemberPublic(member) && CanWriteToMemberWithSetter(member))
                    {
                        var dumper = GetDumper(this, memberValue, member.Name);
                        
                        dumper.AddPublic(memberBuilder, ElementName, member.Name);
                    }
                    else if (CanWriteToMember(member))
                    {
                        var dumper = GetDumper(this, memberValue, member.Name);

                        dumper.AddPrivate(memberBuilder, ElementName, member.Name);
                    }
                }

                codeBuilder.AddCode(memberBuilder);
            }
        }

        public override void GetPrivateInitCode(CodeBuilder codeBuilder)
        {
            GetPublicInitCode(codeBuilder);
        }

        public override void AddPublic(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            GetPublicInitCode(codeBuilder);

            var initCode = string.Format("{0}.{1} = {2};", parentName, elementNameInParent, ElementName);
            
            codeBuilder.AddCode(initCode);
        }

        public override void AddPrivate(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            GetPrivateInitCode(codeBuilder);

            var initCode = string.Format("SetValue({0}, \"{1}\", {2});", parentName, elementNameInParent, ElementName);
            
            codeBuilder.AddCode(initCode);
        }
    }
}
