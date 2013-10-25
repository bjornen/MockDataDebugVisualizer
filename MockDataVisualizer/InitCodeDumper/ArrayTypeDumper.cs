using System;
using System.Collections;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class ArrayTypeDumper : Dumper
    {
        public ArrayTypeDumper(Dumper parent, object element, string name) : base(parent, element, name)
        {
            ElementName = string.Format("{0}_{1}", name, ObjectCounter++);
        }

        public override string GetPublicInitCode()
        {
            var genericArguments = Type.GetGenericArguments();

            string initCode = string.Empty, memberInitCode = string.Empty;

            int count = 0;

            var enumerableElement = Element as IEnumerable;

            foreach (var element in enumerableElement)
            {
                count++;
                var dumper = GetDumper(this, element, element.GetType().Name);

                var elementInitCode = dumper.GetPublicInitCode();

                if (element is ValueType)
                {
                    memberInitCode = string.Format("{0}{1}{2}[{3}] = {4};", memberInitCode, Environment.NewLine, ElementName, count - 1, elementInitCode);
                }
                else
                {
                    memberInitCode = string.Format("{0}{1}{2}", memberInitCode, Environment.NewLine, elementInitCode);
                    memberInitCode = string.Format("{0}{1}{2}[{3}] = {4};", memberInitCode, Environment.NewLine, ElementName, count - 1, dumper.ElementName);
                }
            }

            if (genericArguments.Length == 0)
            {
                initCode = string.Format("{0}var {1} = new {2}[{3}];{4}", Environment.NewLine, ElementName, TypeName.Substring(0, TypeName.Length - 2), count, memberInitCode);
            }

            return string.Format("{0}{1}", Environment.NewLine, initCode);
        }

        public override string GetPrivateInitCode()
        {
            throw new NotImplementedException();
        }

        public override string AddPublic(string initCode, string parentName, string elementNameInParent)
        {
            return string.Format("{0}{1}{2}{3}{4}.{5} = {6};", initCode, Environment.NewLine, GetPublicInitCode(), Environment.NewLine, parentName, elementNameInParent, ElementName);
        }

        public override string AddPrivate(string initCode, string parentName, string elementNameInParent)
        {
            return string.Format("{0}{1}{2}{3}SetValue({4}, \"{5}\", {6});", initCode, Environment.NewLine, GetPublicInitCode(), Environment.NewLine, parentName, elementNameInParent, ElementName);
        }
    }
}
