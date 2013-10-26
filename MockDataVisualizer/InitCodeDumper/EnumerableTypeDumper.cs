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

        public override void GetPublicInitCode(CodeBuilder codeBuilder)
        {
            var genericArguments = Type.GetGenericArguments();

            var initCode = string.Empty;

            var enumerableElement = Element as IEnumerable;

            if (genericArguments.Length == 1)
            {
                var genericArgument = Type.GetGenericArguments()[0].Name;

                initCode = string.Format("var {0} = new {1}<{2}>();", ElementName, TypeName.Substring(0, TypeName.Length - 2), genericArgument);    
            }

            foreach (var element in enumerableElement)
            {
                var rep = GetDumper(this, element, element.GetType().Name);

                var elementInitCode = rep.GetPublicInitCode(codeBuilder);
                
                if (element is ValueType)
                {
                    initCode = string.Format("{0}{1}{2}.Add({3});", initCode, Environment.NewLine, ElementName, elementInitCode);    
                }
                else
                {
                    initCode = string.Format("{0}{1}{2}", initCode, Environment.NewLine, elementInitCode);
                    initCode = string.Format("{0}{1}{2}.Add({3});", initCode, Environment.NewLine, ElementName, rep.ElementName);    
                }
            }

            return string.Format("{0}{1}", Environment.NewLine, initCode);
        }

        public override string GetPrivateInitCode()
        {
            throw new NotImplementedException();
        }

        public override string AddPublic(string initCode, string parentName, string elementNameInParent)
        {
            return string.Format("{0}{1}{2}{3}{4}.{5} = {6};", initCode, Environment.NewLine, GetPublicInitCode(TODO), Environment.NewLine, parentName, elementNameInParent, ElementName);
        }

        public override string AddPrivate(string initCode, string parentName, string elementNameInParent)
        {
            return string.Format("{0}{1}{2}{3}SetValue({4}, \"{5}\", {6});", initCode, Environment.NewLine, GetPublicInitCode(TODO), Environment.NewLine, parentName, elementNameInParent, ElementName);
        }
    }
}
