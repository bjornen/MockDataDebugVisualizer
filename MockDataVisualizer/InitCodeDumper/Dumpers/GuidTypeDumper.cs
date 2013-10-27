namespace MockDataDebugVisualizer.InitCodeDumper.Dumpers
{
    public class GuidTypeDumper : AbstractOneLineInitDumper
    {
        public GuidTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name) { }

        public override string PublicOneLineInitCode()
        {
            return string.Format("Guid.Parse(\"{0}\")", Element);
        }

        public override string PrivateOneLineInitCode()
        {
            return PublicOneLineInitCode();
        }
    }
}
