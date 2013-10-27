using System;
using System.Collections;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class EnumerableTypeDumper : Dumper
    {
        public EnumerableTypeDumper(Dumper parent, object element, string name) : base(parent, element, name)
        {
            ElementName = string.Format("{0}_{1}", name, ObjectCounter++);
        }

        public void ResolveMembers(CodeBuilder codeBuilder)
        {
            var enumerableElement = Element as IEnumerable;

            foreach (var element in enumerableElement)
            {
                var rep = GetDumper(this, element, element.GetType().Name);

                var oneLineRep = rep as IOneLineInit;
                
                if (oneLineRep != null)
                {
                    var publicInitCode = oneLineRep.PublicOneLineInitCode();

                    var initCode = string.Format("{0}.Add({1});", ElementName, publicInitCode);    

                    codeBuilder.AddCode(initCode);
                }
                else
                {
                    rep.AddPublic(codeBuilder, null, null);

                    var initCode = string.Format("{0}.Add({1});", ElementName, rep.ElementName);

                    codeBuilder.AddCode(initCode);
                }
            }
        }

        private void ResolveTypeInitilization(CodeBuilder codeBuilder)
        {
            var genericArguments = Type.GetGenericArguments();

            if (genericArguments.Length == 1)
            {
                var genericArgument = Type.GetGenericArguments()[0].Name;

                var initCode = string.Format("var {0} = new {1}<{2}>();", ElementName,
                    TypeName.Substring(0, TypeName.Length - 2), genericArgument);

                codeBuilder.AddCode(initCode);
            }
        }

        public override void AddPublic(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            ResolveTypeInitilization(codeBuilder);

            ResolveMembers(codeBuilder);

            if (!string.IsNullOrWhiteSpace(parentName))
            {
                var initCode = string.Format("{0}.{1} = {2};", parentName, elementNameInParent, ElementName);

                codeBuilder.AddCode(initCode);
            }
        }

        public override void AddPrivate(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            ResolveTypeInitilization(codeBuilder);

            ResolveMembers(codeBuilder);

            var line = string.Format("SetValue({0}, \"{1}\", {2});", parentName, elementNameInParent, ElementName);

            codeBuilder.AddCode(line);
        }
    }
}
