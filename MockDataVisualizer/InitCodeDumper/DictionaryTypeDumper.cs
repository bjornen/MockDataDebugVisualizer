using System.Collections;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class DictionaryTypeDumper : AbstractComplexTypeDumper
    {
        public DictionaryTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name)
        {
            ElementName = string.Format("{0}_{1}", name, ObjectCounter++);
        }

        public override void ResolveMembers(CodeBuilder codeBuilder)
        {
            var enumerableElement = Element as IEnumerable;

            foreach (var element in enumerableElement)
            {
                dynamic keyValue = element;

                var keyName = string.Format("{0}_{1}", keyValue.Key.GetType().Name, ObjectCounter++);
                var valueName = string.Format("{0}_{1}", keyValue.Value.GetType().Name, ObjectCounter++);
                
                var keyRep = GetDumper(this, keyValue.Key, keyName);
                var valueRep = GetDumper(this, keyValue.Value, valueName);

                var oneLineKeyDumper = keyRep as IOneLineInitDumper;
                var oneLineValueDumper = valueRep as IOneLineInitDumper;
                
                if (oneLineKeyDumper == null)
                {
                    keyRep.AddPublicMember(codeBuilder);
                }
                if (oneLineValueDumper == null)
                {
                    valueRep.AddPublicMember(codeBuilder);
                }

                string line;

                if (oneLineKeyDumper != null)
                {
                    line = string.Format("{0}.Add({1},", ElementName, oneLineKeyDumper.PublicOneLineInitCode());
                }
                else
                {
                    line = string.Format("{0}.Add({1},", ElementName, keyRep.ElementName);
                }

                if (oneLineValueDumper != null)
                {
                    line = string.Format("{0} {1});", line, oneLineValueDumper.PublicOneLineInitCode());
                }
                else
                {
                    line = string.Format("{0} {1});", line, valueRep.ElementName);
                }

                codeBuilder.AddCode(line);
            }
        }

        public override void ResolveTypeInitilization(CodeBuilder codeBuilder)
        {
            var genericArguments = Element.GetType().GetGenericArguments();

            var typeName = Element.GetType().Name;

            if (genericArguments.Length == 2)
            {
                var initCode = string.Format("var {0} = new {1}<{2}, {3}>();", ElementName,
                    typeName.Substring(0, typeName.Length - 2), Element.GetType().GetGenericArguments()[0].Name,
                    Element.GetType().GetGenericArguments()[1].Name);

                codeBuilder.AddCode(initCode);
            }
        }

        public override void AddPublicMemberAndAssignToParent(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            ResolveTypeInitilization(codeBuilder);

            ResolveMembers(codeBuilder);

            if (!string.IsNullOrWhiteSpace(parentName))
            {
                var initCode = string.Format("{0}.{1} = {2};", parentName, elementNameInParent, ElementName);

                codeBuilder.AddCode(initCode);
            }
        }

        public override void AddPrivateMemberAndAssignToParrent(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            ResolveTypeInitilization(codeBuilder);

            ResolveMembers(codeBuilder);

            var line = string.Format("SetValue({0}, \"{1}\", {2});", parentName, elementNameInParent, ElementName);

            codeBuilder.AddCode(line);
        }
    }
}
