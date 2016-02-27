using System;

namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class DateTimeType : DumperBase
    {
        public DateTimeType(object element, string name) : base(element, name) {}

        public override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            var dt = Element as DateTime? ?? new DateTime();

            codeBuilder.PushInitValue($"new DateTime({dt.Ticks})");
        }
    }
}
