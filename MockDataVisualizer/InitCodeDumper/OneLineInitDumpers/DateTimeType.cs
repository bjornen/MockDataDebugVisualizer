using System;

namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class DateTimeType : DumperBase
    {
        public DateTimeType(object element, string name) : base(element, name) {}

        public override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            var dt = Element is DateTime ? (DateTime)Element : new DateTime();

            codeBuilder.PushInitValue(string.Format("new DateTime({0})", dt.Ticks));
        }
    }
}
