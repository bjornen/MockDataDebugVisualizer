using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class GuidTypeDumper : Dumper
    {
        public GuidTypeDumper(Dumper parent, object element, string name) : base(parent, element, name)
        {
        }

        public string GetPublicInitCode()
        {
            return string.Format("Guid.Parse(\"{0}\")", Element);
        }

        public string GetPrivateInitCode()
        {
            return GetPublicInitCode();
        }

        public override void AddPrivate(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            throw new NotImplementedException();
        }

        public override void AddPublic(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPublicInitCode();
            
            var initCode = string.Format("{0}.{1} = {2};", parentName, ElementName, memberInitCode);
            
            codeBuilder.AddCode(initCode);
        }
    }
}
