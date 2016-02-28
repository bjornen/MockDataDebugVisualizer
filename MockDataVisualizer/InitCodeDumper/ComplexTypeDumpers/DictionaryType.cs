using System.Collections;

namespace MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers
{
    public class DictionaryType : AbstractComplexType
    {
        public DictionaryType(object element, string name) : base(element, name) { }

        public override void ResolveTypeInitilization(CodeBuilder codeBuilder)
        {
            var genericArguments = Element.GetType().GetGenericArguments();

            if (genericArguments.Length == 2)
            {
                var initCode = InitCode();

                codeBuilder.AddCode(initCode);
            }
        }

        private string InitCode()
        {
            return $"var {ElementName} = new {MemberName}<{GenericTypeName(0)}, {GenericTypeName(1)}>();";
        }

        public override void ResolveMembers(CodeBuilder codeBuilder)
        {
            var enumerableElement = Element as IEnumerable;

            foreach (var element in enumerableElement)
            {
                dynamic keyValue = element;

                var keyDumper = DumperFactory.GetDumper(keyValue.Key, keyValue.Key.GetType().Name);

                var valueDumper = DumperFactory.GetDumper(keyValue.Value, keyValue.Value.GetType().Name);

                keyDumper.ResolveInitCode(codeBuilder);

                var line = $"{ElementName}.Add({codeBuilder.PopInitValue()},";

                valueDumper.ResolveInitCode(codeBuilder);
   
                line = $"{line} {codeBuilder.PopInitValue()});";

                codeBuilder.AddCode(line);
            }
        }
    }
}
