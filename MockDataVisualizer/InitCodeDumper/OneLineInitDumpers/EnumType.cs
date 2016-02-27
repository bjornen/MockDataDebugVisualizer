using System;

namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class EnumType : DumperBase
    {
        public EnumType(object element, string name) : base(element, name) { }

        public override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            codeBuilder.PushInitValue($"{Element.GetType().Name}.{Convert.ToString(Element)}");
        }
    }
}
