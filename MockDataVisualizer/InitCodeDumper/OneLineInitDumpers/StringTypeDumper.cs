
namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class StringTypeDumper : DumperBase
    {
        public StringTypeDumper(object element, string name) : base(element, name){}

        internal override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            codeBuilder.PushInitValue(string.Format("\"{0}\"", Element));
        }
    }
}
