
namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class StringTypeDumper : Dumper, IOneLineInit
    {
        public StringTypeDumper(Dumper parent, object element, string name) : base(parent, element, name){}

        public string PublicOneLineInitCode()
        {
            return string.Format("\"{0}\"", Element);
        }

        public string PrivateOneLineInitCode()
        {
            return string.Format("SetValue({0}, \"{1}\", \"{2}\")", Parent.ElementName, ElementName, Element);
        }

        public override void AddPrivate(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var memberInitCode = PrivateOneLineInitCode();
            
            var initCode = string.Format("{0};", memberInitCode);
            
            codeBuilder.AddCode(initCode);
        }

        public override void AddPublic(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var memberInitCode = PublicOneLineInitCode();

            var initCode = string.Format("{0}.{1} = {2};", parentName, ElementName, memberInitCode);
            
            codeBuilder.AddCode(initCode);
        }
    }
}
