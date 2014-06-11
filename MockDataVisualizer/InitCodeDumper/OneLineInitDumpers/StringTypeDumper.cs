
namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class StringTypeDumper : DumperBase
    {
        public StringTypeDumper(object element, string name) : base(element, name){}

        public override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            codeBuilder.PushInitValue(Element == null ? "null" : string.Format("\"{0}\"", Element));
        }
    }
}