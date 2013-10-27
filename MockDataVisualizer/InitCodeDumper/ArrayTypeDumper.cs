using System;
using System.Collections;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class ArrayTypeDumper : Dumper
    {
        private int _arrayLength;

        public ArrayTypeDumper(Dumper parent, object element, string name) : base(parent, element, name)
        {
            ElementName = string.Format("{0}_{1}", name, ObjectCounter++);
            
            _arrayLength = 0;
        }

        public void ResolveMembers(CodeBuilder codeBuilder)
        {
            var elementList = Element as IList;

            for (var i = 0; i < elementList.Count; i++)
            {
                var dumper = GetDumper(this, elementList[i], elementList[i].GetType().Name);

                var oneLineDumper = dumper as IOneLineInit;

                if (oneLineDumper != null)
                {
                    var memberInitCode = string.Format("{0}[{1}] = {2};", ElementName, i, oneLineDumper.PublicOneLineInitCode());

                    codeBuilder.AddCode(memberInitCode);
                }
                else
                {
                    dumper.AddPublic(codeBuilder, null, null);

                    var memberInitCode = string.Format("{0}[{1}] = {2};", ElementName, i, dumper.ElementName);

                    codeBuilder.AddCode(memberInitCode);
                }
            }
        }

        public void ResolveTypeInitilizationCode(CodeBuilder codeBuilder)
        {
            var genericArguments = Type.GetGenericArguments();

            if (genericArguments.Length == 0)
            {
                var initCode = string.Format("var {0} = new {1}[{2}];", ElementName, TypeName.Substring(0, TypeName.Length - 2), _arrayLength);

                codeBuilder.AddCode(initCode);
            }
        }

        public override void AddPublic(CodeBuilder  codeBuilder, string parentName, string elementNameInParent)
        {
            SetArrayLength();

            ResolveTypeInitilizationCode(codeBuilder);

            ResolveMembers(codeBuilder);

            var line = string.Format("{0}.{1} = {2};", parentName, elementNameInParent, ElementName);

            codeBuilder.AddCode(line);
        }

        public override void AddPrivate(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            SetArrayLength();

            ResolveTypeInitilizationCode(codeBuilder);

            ResolveMembers(codeBuilder);

            var line = string.Format("SetValue({0}, \"{1}\", {2});", parentName, elementNameInParent, ElementName);

            codeBuilder.AddCode(line);
        }

        private void SetArrayLength()
        {
            _arrayLength = (Element as Array).Length;
        }
    }
}
