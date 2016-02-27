using System;

namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class ValueType : DumperBase
    {
        public ValueType(object element, string name) : base(element, name){}

        public override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            var value = $"{Convert.ToString(Element).ToLower()}";

            if (value.Contains(","))
                value = $"{value.Replace(',', '.')}";

            if (Element is uint)
                value = $"{value}U";

            if (Element is ulong)
                value = $"{value}UL";

            if (Element is float)
                value = $"{value}F";

            if (Element is double)
                value = $"{value}D";

            if (Element is decimal)
                value = $"{value}M";

            if (Element is short)
                value = $"(short){value}";

            if (Element is ushort)
                value = $"(ushort){value}";

            if (Element is byte)
                value = $"(byte){value}";

            if (Element is sbyte)
                value = $"(sbyte){value}";

            codeBuilder.PushInitValue(value);
        }
    }
}
