﻿using System.Collections;

namespace MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers
{
    public class EnumerableTypeDumper : AbstractComplexTypeDumper
    {
        public EnumerableTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name) { }

        public override void ResolveTypeInitilization(CodeBuilder codeBuilder)
        {
            var genericArguments = Element.GetType().GetGenericArguments();

            var typeName = Element.GetType().Name;

            if (genericArguments.Length == 1)
            {
                var genericArgument = Element.GetType().GetGenericArguments()[0].Name;

                var initCode = string.Format("var {0} = new {1}<{2}>();", ElementName, typeName.Substring(0, typeName.Length - 2), genericArgument);

                codeBuilder.AddCode(initCode);
            }
        }

        public override void ResolveMembers(CodeBuilder codeBuilder)
        {
            var enumerableElement = Element as IEnumerable;

            foreach (var element in enumerableElement)
            {
                var dumper = GetDumper(this, element, element.GetType().Name);

                var oneLineDumper = dumper as AbstractOneLineInitDumper;
                
                if (oneLineDumper != null)
                {
                    codeBuilder.AddCode(string.Format("{0}.Add({1});", ElementName, oneLineDumper.PublicOneLineInitCode()));
                }

                var complexTypeDumper = dumper as AbstractComplexTypeDumper;
                
                if(complexTypeDumper != null)
                {
                    complexTypeDumper.AddPublicMember(codeBuilder);

                    codeBuilder.AddCode(string.Format("{0}.Add({1});", ElementName, dumper.ElementName));
                }
            }
        }
    }
}