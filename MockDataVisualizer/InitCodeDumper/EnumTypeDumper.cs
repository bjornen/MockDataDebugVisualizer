using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class EnumTypeDumper : Dumper
    {
        public EnumTypeDumper(Dumper parent, object element, string name) : base(parent, element, name)
        {
        }

        public override string GetPublicInitCode()
        {
            return string.Format("{0}.{1}", ElementTypeName, Convert.ToString(Element));
        }

        public override string GetPrivateInitCode()
        {
            return string.Format("SetValue({0}, \"{1}\", {2}.{3})", Parent.ElementName, ElementName, ElementTypeName, Convert.ToString(Element));
        }

        public override string AddPrivate(string initCode, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPrivateInitCode();
            initCode = string.Format("{0}{1}{2};", initCode, Environment.NewLine, memberInitCode);
            return initCode;
        }

        public override string AddPublic(string initCode, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPublicInitCode();
            initCode = string.Format("{0}{1}{2}.{3} = {4};", initCode, Environment.NewLine, parentName, ElementName, memberInitCode);
            return initCode;
        }
    }
}
