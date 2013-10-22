using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class StringTypeDumper : Dumper
    {
        public StringTypeDumper(Dumper parent, object element, string name) : base(parent, element, name){}

        public override string GetPublicInitCodeDump()
        {
            return string.Format("\"{0}\"", Element);
        }

        public override string GetPrivateInitCodeDump()
        {
            return string.Format("SetValue({0}, \"{1}\", \"{2}\")", Parent.ElementName, ElementName, Element);
        }

        public override string DumpPrivate(string initCode, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPrivateInitCodeDump();
            initCode = string.Format("{0}{1}{2};", initCode, Environment.NewLine, memberInitCode);
            return initCode;
        }

        public override string DumpPublic(string initCode, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPublicInitCodeDump();
            initCode = string.Format("{0}{1}{2}.{3} = {4};", initCode, Environment.NewLine, parentName, ElementName, memberInitCode);
            return initCode;
        }
    }
}
