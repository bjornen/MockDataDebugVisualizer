using System;
using System.Collections;

namespace MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers
{
    public class ArrayTypeDumper : AbstractComplexTypeDumper
    {
        private readonly int _arrayLength;

        public ArrayTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name)
        {
            _arrayLength = (Element as Array).Length;
        }

        public override void ResolveTypeInitilization(CodeBuilder codeBuilder)
        {
            var genericArguments = Element.GetType().GetGenericArguments();

            var typeName = Element.GetType().Name;

            if (genericArguments.Length == 0)
            {
                var initCode = string.Format("var {0} = new {1}[{2}];", ElementName, typeName.Substring(0, typeName.Length - 2), _arrayLength);

                codeBuilder.AddCode(initCode);
            }
        }

        public override void ResolveMembers(CodeBuilder codeBuilder)
        {
            var elementList = Element as IList;

            for (var i = 0; i < elementList.Count; i++)
            {
                var dumper = GetDumper(this, elementList[i], elementList[i].GetType().Name);

                var oneLineDumper = dumper as AbstractOneLineInitDumper;

                if (oneLineDumper != null)
                {
                    var memberInitCode = string.Format("{0}[{1}] = {2};", ElementName, i, oneLineDumper.PublicOneLineInitCode());

                    codeBuilder.AddCode(memberInitCode);
                }
                
                var objectDumper = dumper as AbstractComplexTypeDumper;
                
                if(objectDumper != null)
                {
                    objectDumper.AddPublicMember(codeBuilder);

                    var memberInitCode = string.Format("{0}[{1}] = {2};", ElementName, i, dumper.ElementName);

                    codeBuilder.AddCode(memberInitCode);
                }
            }
        }
    }
}
