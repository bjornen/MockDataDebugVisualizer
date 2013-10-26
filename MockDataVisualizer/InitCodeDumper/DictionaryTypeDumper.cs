using System.Collections;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class DictionaryTypeDumper : Dumper
    {
        public DictionaryTypeDumper(Dumper parent, object element, string name) : base(parent, element, name)
        {
            ElementName = string.Format("{0}_{1}", name, ObjectCounter++);
        }

        public void ResolveMembers(CodeBuilder codeBuilder)
        {
            var enumerableElement = Element as IEnumerable;

            foreach (var element in enumerableElement)
            {
                dynamic keyValue = element;

                var keyName = string.Format("{0}_{1}", keyValue.Key.GetType().Name, ObjectCounter++);
                var valueName = string.Format("{0}_{1}", keyValue.Value.GetType().Name, ObjectCounter++);
                
                var keyRep = GetDumper(this, keyValue.Key, keyName);
                var valueRep = GetDumper(this, keyValue.Value, valueName);

                var oneLineKeyDumper = keyRep as IOneLineInit;
                var oneLineValueDumper = valueRep as IOneLineInit;
                
                if (oneLineKeyDumper == null)
                {
                    keyRep.AddPublic(codeBuilder, null, null);
                }
                if (oneLineValueDumper == null)
                {
                    valueRep.AddPublic(codeBuilder, null, null);
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

        private void ResolveInitilizationCode(CodeBuilder codeBuilder)
        {
            var genericArguments = Type.GetGenericArguments();

            if (genericArguments.Length == 2)
            {
                var initCode = string.Format("var {0} = new {1}<{2}, {3}>();", ElementName,
                    TypeName.Substring(0, TypeName.Length - 2), Type.GetGenericArguments()[0].Name,
                    Type.GetGenericArguments()[1].Name);

                codeBuilder.AddCode(initCode);
            }
        }

        public override void AddPublic(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            ResolveInitilizationCode(codeBuilder);

            ResolveMembers(codeBuilder);

            if (!string.IsNullOrWhiteSpace(parentName))
            {
                var initCode = string.Format("{0}.{1} = {2};", parentName, elementNameInParent, ElementName);

                codeBuilder.AddCode(initCode);
            }
        }

        public override void AddPrivate(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            ResolveInitilizationCode(codeBuilder);

            ResolveMembers(codeBuilder);

            var line = string.Format("SetValue({0}, \"{1}\", {2});", parentName, elementNameInParent, ElementName);

            codeBuilder.AddCode(line);
        }
    }
}
