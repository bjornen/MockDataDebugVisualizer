using System;

namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class DateTimeTypeDumper : AbstractOneLineInitDumper
    {
        public DateTimeTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name) {}

        public override string PublicOneLineInitCode()
        {
            var dt = Element is DateTime ? (DateTime) Element : new DateTime();

            return string.Format("new DateTime({0})", dt.Ticks);
        }
    }
}
