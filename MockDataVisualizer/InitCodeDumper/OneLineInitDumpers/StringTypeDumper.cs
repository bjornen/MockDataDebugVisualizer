
namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class StringTypeDumper : AbstractOneLineInitDumper
    {
        public StringTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name){}

        public override string PublicOneLineInitCode()
        {
            return string.Format("\"{0}\"", Element);
        }

        public override string PrivateOneLineInitCode()
        {
            return string.Format("SetValue({0}, \"{1}\", \"{2}\")", Parent.ElementName, ElementName, Element);
        }
    }
}
