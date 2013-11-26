using System;

namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class EnumTypeDumper : DumperBase
    {
        public EnumTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name) { }

        internal override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            codeBuilder.PushInitValue(string.Format("{0}.{1}", Element.GetType().Name, Convert.ToString(Element)));
        }
    }
}
