using System;

namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class ValueTypeDumper : DumperBase
    {
        public ValueTypeDumper(object element, string name) : base(element, name){}

        public override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            var value = string.Format("{0}", Convert.ToString(Element).ToLower());

            if (value.Contains(","))
                value = string.Format("{0}", value.Replace(',', '.'));

            if (Element is uint)
                value = string.Format("{0}U", value);

            if (Element is ulong)
                value = string.Format("{0}UL", value);

            if (Element is float)
                value = string.Format("{0}F", value);

            if (Element is double)
                value = string.Format("{0}D", value);

            if (Element is decimal)
                value = string.Format("{0}M", value);

            if (Element is short)
                value = string.Format("(short){0}", value);

            if (Element is ushort)
                value = string.Format("(ushort){0}", value);

            if (Element is byte)
                value = string.Format("(byte){0}", value);

            if (Element is sbyte)
                value = string.Format("(sbyte){0}", value);

            codeBuilder.PushInitValue(value);
        }
    }
}
