
namespace MockDataDebugVisualizer.InitCodeDumper.OneLineInitDumpers
{
    public class StringTypeDumper : DumperBase
    {
        public StringTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name){}

        internal override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            codeBuilder.PushInitValue(string.Format("\"{0}\"", Element));
        }
    }
}
