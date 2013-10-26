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

        public override string GetPublicInitCode()
        {
            string initCode;
            var typeName = ResolveInitTypeName(Element.GetType());

            if (Type.GetConstructor(Type.EmptyTypes) == null)
            {
                initCode = string.Format("var {0} = ({1}) FormatterServices.GetUninitializedObject(typeof ({1}));", ElementName, typeName);
            }
            else
            {
                initCode = string.Format("var {0} = new {1}();", ElementName, typeName);    
            }
            
            foreach (var member in Members)
            {
                var memberValue = GetMemberValue(member);

                if (memberValue == null) continue;

                var memberInitCode = string.Empty;

                if (IsElementAlreadyTouched(memberValue))
                {
                    if (IsMemberPublic(member) && CanWriteToMemberWithSetter(member))
                    {
                        memberInitCode = string.Format("{0}.{1} = {2};", ElementName, member.Name, GetNameOfAlreadyTouchedElement(memberValue));
                    }
                    else if (CanWriteToMember(member))
                    {
                        memberInitCode = string.Format("SetValue({0}, \"{1}\", {2});", ElementName, member.Name, GetNameOfAlreadyTouchedElement(memberValue));
                    }
                }
                else
                {
                    if (IsMemberPublic(member) && CanWriteToMemberWithSetter(member))
                    {
                        var dumper = GetDumper(this, memberValue, member.Name);
                        
                        initCode = dumper.AddPublic(initCode, ElementName, member.Name);
                    }
                    else if (CanWriteToMember(member))
                    {
                        var dumper = GetDumper(this, memberValue, member.Name);

                        initCode = dumper.AddPrivate(initCode, ElementName, member.Name);
                    }
                }

                if (!string.IsNullOrEmpty(memberInitCode))
                {
                    initCode = string.Format("{0}{1}{2}", initCode, Environment.NewLine, memberInitCode);
                }
            }

            return initCode;
        }

        public override string GetPrivateInitCode()
        {
            return GetPublicInitCode();
        }

        public override string AddPublic(string initCode, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPublicInitCode();

            initCode = string.Format("{0}{1}{2}", initCode, Environment.NewLine, memberInitCode);
            
            initCode = string.Format("{0}{1}{2}.{3} = {4};", initCode, Environment.NewLine, parentName, elementNameInParent, ElementName);
            
            return initCode;
        }

        public override string AddPrivate(string initCode, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPrivateInitCode();

            initCode = string.Format("{0}{1}{2}{3}SetValue({4}, \"{5}\", {6});", initCode, Environment.NewLine, memberInitCode, Environment.NewLine, parentName, elementNameInParent, ElementName);
            
            return initCode;
        }
    }
}
