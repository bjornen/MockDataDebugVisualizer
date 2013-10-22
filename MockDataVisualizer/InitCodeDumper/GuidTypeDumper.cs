using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class GuidTypeDumper : Dumper
    {
        public GuidTypeDumper(Dumper parent, object element, string name) : base(parent, element, name)
        {
        }

        public override string GetPublicInitCodeDump()
        {
            return string.Format("Guid.Parse(\"{0}\")", Element);
        }

        public override string GetPrivateInitCodeDump()
        {
            return GetPublicInitCodeDump();
        }

        public override string DumpPrivate(string initCode, string parentName, string elementNameInParent)
        {
            throw new NotImplementedException();
        }

        public override string DumpPublic(string initCode, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPublicInitCodeDump();
            initCode = string.Format("{0}{1}{2}.{3} = {4};", initCode, Environment.NewLine, parentName, ElementName, memberInitCode);
            return initCode;
        }
    }
}
