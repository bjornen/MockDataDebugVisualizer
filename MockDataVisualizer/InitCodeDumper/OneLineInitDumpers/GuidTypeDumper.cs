namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class GuidTypeDumper : DumperBase
    {
        public GuidTypeDumper(object element, string name) : base(element, name) { }

        public override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            codeBuilder.PushInitValue(string.Format("Guid.Parse(\"{0}\")", Element));
        }
    }
}
