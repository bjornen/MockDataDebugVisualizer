using System;

namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class EnumTypeDumper : DumperBase
    {
        public EnumTypeDumper(object element, string name) : base(element, name) { }

        public override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            codeBuilder.PushInitValue(string.Format("{0}.{1}", Element.GetType().Name, Convert.ToString(Element)));
        }
    }
}
