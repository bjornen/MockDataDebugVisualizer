using System;
using System.Collections;

namespace MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers
{
    public class ArrayType : AbstractComplexType
    {
        public ArrayType(object element, string name) : base(element, name)
        {
        }

        public override void ResolveTypeInitilization(CodeBuilder codeBuilder)
        {
            var genericArguments = Element.GetType().GetGenericArguments();

            if (genericArguments.Length == 0)
            {
                var arrayLength = (Element as Array).Length;

                var initCode = InitCode(arrayLength);

                codeBuilder.AddCode(initCode);
            }
        }

        private string InitCode(int arrayLength)
        {
            return $"var {ElementName} = new {MemberName}[{arrayLength}];";
        }

        public override void ResolveMembers(CodeBuilder codeBuilder)
        {
            var elementList = Element as IList;

            for (var i = 0; i < elementList.Count; i++)
            {
                if(elementList[i] == null) continue;

                var dumper = GetDumper(elementList[i], elementList[i].GetType().Name);

                dumper.ResolveInitCode(codeBuilder);

                codeBuilder.AddCode($"{ElementName}[{i}] = {codeBuilder.PopInitValue()};");
            }
        }
    }
}
