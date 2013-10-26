using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class EnumTypeDumper : Dumper
    {
        public EnumTypeDumper(Dumper parent, object element, string name) : base(parent, element, name)
        {
        }

        public string GetPublicInitCode()
        {
            return string.Format("{0}.{1}", ElementTypeName, Convert.ToString(Element));
        }

        public string GetPrivateInitCode()
        {
            return string.Format("SetValue({0}, \"{1}\", {2}.{3})", Parent.ElementName, ElementName, ElementTypeName, Convert.ToString(Element));
        }

        public override void AddPrivate(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPrivateInitCode();

            var initCode = string.Format("{0};", memberInitCode);
            
            codeBuilder.AddCode(initCode);
        }

        public override void AddPublic(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPublicInitCode();
            
            var initCode = string.Format("{0}.{1} = {2};", parentName, ElementName, memberInitCode);
            
            codeBuilder.AddCode(initCode);
        }
    }
}
