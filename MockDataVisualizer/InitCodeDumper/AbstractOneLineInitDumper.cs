
namespace MockDataDebugVisualizer.InitCodeDumper
{
    public abstract class AbstractOneLineInitDumper : DumperBase
    {
        protected AbstractOneLineInitDumper(DumperBase parent, object element, string name) : base(parent, element, name) { }

        public abstract string PublicOneLineInitCode();

        public virtual string PrivateOneLineInitCode()
        {
            return string.Format("SetValue({0}, \"{1}\", {2})", Parent.ElementName, ElementName, PublicOneLineInitCode());
        }

        internal override void AddPublicMember(CodeBuilder codeBuilder)
        {
            codeBuilder.PushCode(PublicOneLineInitCode());
        }

        internal override void AddPrivateMember(CodeBuilder codeBuilder)
        {
            codeBuilder.PushCode(PrivateOneLineInitCode());
        }
    }
}
