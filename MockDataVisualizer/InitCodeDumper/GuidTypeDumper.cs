using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class GuidTypeDumper : DumperBase, IOneLineInitDumper
    {
        public GuidTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name)
        {
        }

        public override void AddPrivateMemberAndAssignToParrent(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var line = string.Format("SetValue({0}, \"{1}\", {2});", parentName, elementNameInParent, PrivateOneLineInitCode());
            
            codeBuilder.AddCode(line);
        }

        public override void AddPublicMemberAndAssignToParent(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var memberInitCode = PublicOneLineInitCode();
            
            var initCode = string.Format("{0}.{1} = {2};", parentName, ElementName, memberInitCode);
            
            codeBuilder.AddCode(initCode);
        }

        public string PublicOneLineInitCode()
        {
            return string.Format("Guid.Parse(\"{0}\")", Element);
        }

        public string PrivateOneLineInitCode()
        {
            return PublicOneLineInitCode();
        }
    }
}
