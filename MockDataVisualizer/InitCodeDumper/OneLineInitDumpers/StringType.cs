
namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class StringType : DumperBase
    {
        public StringType(object element, string name) : base(element, name){}

        public override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            codeBuilder.PushInitValue(Element == null ? "null" : string.Format("\"{0}\"", Element));
        }
    }
}