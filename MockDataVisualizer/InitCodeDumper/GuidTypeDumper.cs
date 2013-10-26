using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class GuidTypeDumper : Dumper
    {
        public GuidTypeDumper(Dumper parent, object element, string name) : base(parent, element, name)
        {
        }

        public override void GetPublicInitCode(CodeBuilder codeBuilder)
        {
            return string.Format("Guid.Parse(\"{0}\")", Element);
        }

        public override string GetPrivateInitCode()
        {
            return GetPublicInitCode(TODO);
        }

        public override string AddPrivate(string initCode, string parentName, string elementNameInParent)
        {
            throw new NotImplementedException();
        }

        public override string AddPublic(string initCode, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPublicInitCode(TODO);
            initCode = string.Format("{0}{1}{2}.{3} = {4};", initCode, Environment.NewLine, parentName, ElementName, memberInitCode);
            return initCode;
        }
    }
}
