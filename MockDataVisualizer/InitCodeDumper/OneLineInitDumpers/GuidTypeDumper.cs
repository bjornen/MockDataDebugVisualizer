namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class GuidTypeDumper : AbstractOneLineInitDumper
    {
        public GuidTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name) { }

        public override string PublicOneLineInitCode()
        {
            return string.Format("Guid.Parse(\"{0}\")", Element);
        }
    }
}
