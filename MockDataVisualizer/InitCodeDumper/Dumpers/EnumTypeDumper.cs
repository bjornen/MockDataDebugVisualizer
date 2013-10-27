using System;

namespace MockDataDebugVisualizer.InitCodeDumper.Dumpers
{
    public class EnumTypeDumper : AbstractOneLineInitDumper
    {
        public EnumTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name) { }

        public override string PublicOneLineInitCode()
        {
            return string.Format("{0}.{1}", Element.GetType().Name, Convert.ToString(Element));
        }

        public override string PrivateOneLineInitCode()
        {
            return string.Format("SetValue({0}, \"{1}\", {2}.{3})", Parent.ElementName, ElementName, Element.GetType().Name, Convert.ToString(Element));
        }
    }
}
