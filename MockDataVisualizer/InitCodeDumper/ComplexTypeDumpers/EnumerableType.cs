using System.Collections;

namespace MockDataDebugVisualizer.InitCodeDumper.ComplexTypeDumpers
{
    public class EnumerableType : AbstractComplexType
    {
        public EnumerableType(object element, string name) : base(element, name) { }

        public override void ResolveTypeInitilization(CodeBuilder codeBuilder)
        {
            var genericArguments = Element.GetType().GetGenericArguments();

            if (genericArguments.Length == 1)
            {
                var initCode = InitCode();

                codeBuilder.AddCode(initCode);
            }
        }

        private string InitCode()
        {
            return $"var {ElementName} = new {MemberName}<{GenericTypeName(0)}>();";
        }

        public override void ResolveMembers(CodeBuilder codeBuilder)
        {
            var enumerableElement = Element as IEnumerable;

            foreach (var element in enumerableElement)
            {
                if(element == null) continue;

                var dumper = DumperFactory.GetDumper(element, element.GetType().Name);

                dumper.ResolveInitCode(codeBuilder);

                codeBuilder.AddCode($"{ElementName}.Add({codeBuilder.PopInitValue()});");
            }
        }
    }
}
