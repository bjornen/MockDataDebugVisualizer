
namespace MockDataDebugVisualizer.InitCodeDumper
{
    public abstract class AbstractComplexTypeDumper : DumperBase
    {
        protected AbstractComplexTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name){ }

        public abstract void ResolveTypeInitilization(CodeBuilder codeBuilder);
        public abstract void ResolveMembers(CodeBuilder codeBuilder);

        internal override void AddPublicMember(CodeBuilder codeBuilder)
        {
            ResolveTypeInitilization(codeBuilder);

            ResolveMembers(codeBuilder);
        }

        internal override void AddPrivateMember(CodeBuilder codeBuilder)
        {
            ResolveTypeInitilization(codeBuilder);

            ResolveMembers(codeBuilder);
        }
    }
}
