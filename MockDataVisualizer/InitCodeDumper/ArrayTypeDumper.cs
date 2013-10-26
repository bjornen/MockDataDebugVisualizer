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
            var enumerableElement = Element as IEnumerable;

            var length = 0;

            foreach (var element in enumerableElement)
            {
                length++;

                var dumper = GetDumper(this, element, element.GetType().Name);

                var oneLineDumper = dumper as IOneLineInit;

                if (oneLineDumper != null)
                {
                    var memberInitCode = string.Format("{0}[{1}] = {2};", ElementName, length - 1, oneLineDumper.PublicOneLineInitCode());

                    codeBuilder.AddCode(memberInitCode);
                }
                else
                {
                    dumper.AddPublic(codeBuilder, null, null);

                    var memberInitCode = string.Format("{0}[{1}] = {2};", ElementName, length - 1, dumper.ElementName);

                    codeBuilder.AddCode(memberInitCode);
                }
            }
        }

        private void ResolveTypeInitilizationCode(CodeBuilder codeBuilder)
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
            var enumerable = Element as Array;

            _arrayLength = enumerable.Length;
        }
    }
}
