namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class GuidTypeDumper : DumperBase
    {
        public GuidTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name) { }

        internal override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            codeBuilder.PushInitValue(string.Format("Guid.Parse(\"{0}\")", Element));
        }
    }
}
