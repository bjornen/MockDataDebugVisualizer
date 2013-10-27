
namespace MockDataDebugVisualizer.InitCodeDumper
{
    public abstract class AbstractComplexTypeDumper : DumperBase
    {
        protected AbstractComplexTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name)
        {
            var typeName = name;

            if (IsGenericType(element.GetType()))
            {
                typeName = ResolveTypeName(element.GetType());
            }

            ElementName = string.Format("{0}_{1}", LowerCaseFirst(typeName), ObjectCounter++);
        }

        public abstract void ResolveTypeInitilization(CodeBuilder codeBuilder);
        public abstract void ResolveMembers(CodeBuilder codeBuilder);

        internal override void AddPublicMember(CodeBuilder codeBuilder)
        {
            ResolveTypeInitilization(codeBuilder);

            ResolveMembers(codeBuilder);

            codeBuilder.PushInitValue(ElementName);
        }

        internal override void AddPrivateMember(CodeBuilder codeBuilder)
        {
            AddPublicMember(codeBuilder);
        }

        private static string LowerCaseFirst(string variableName)
        {
            return string.IsNullOrEmpty(variableName) ? string.Empty : char.ToLower(variableName[0]) + variableName.Substring(1);
        }
    }
}
