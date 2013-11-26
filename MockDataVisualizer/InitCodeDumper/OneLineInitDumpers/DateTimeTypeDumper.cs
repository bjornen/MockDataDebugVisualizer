using System;

namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class DateTimeTypeDumper : DumperBase
    {
        public DateTimeTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name) {}

        internal override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            var dt = Element is DateTime ? (DateTime)Element : new DateTime();

            codeBuilder.PushInitValue(string.Format("new DateTime({0})", dt.Ticks));
        }
    }
}
