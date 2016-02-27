namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class GuidType : DumperBase
    {
        public GuidType(object element, string name) : base(element, name) { }

        public override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            codeBuilder.PushInitValue($"Guid.Parse(\"{Element}\")");
        }
    }
}
