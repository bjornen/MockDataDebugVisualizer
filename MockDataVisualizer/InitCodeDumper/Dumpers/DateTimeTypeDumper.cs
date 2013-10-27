using System;

namespace MockDataDebugVisualizer.InitCodeDumper.Dumpers
{
    public class DateTimeTypeDumper : AbstractOneLineInitDumper
    {
        public DateTimeTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name) {}

        public override string PublicOneLineInitCode()
        {
            var dt = Element is DateTime ? (DateTime) Element : new DateTime();

            return string.Format("new DateTime({0})", dt.Ticks);
        }

        public override string PrivateOneLineInitCode()
        {
            var publicInitCode = PublicOneLineInitCode();

            return string.Format("SetValue({0}, \"{1}\", {2})", Parent.ElementName, ElementName, publicInitCode);
        }

        //public override void AddPrivateMemberAndAssignToParrent(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        //{
        //    var memberInitCode = PrivateOneLineInitCode();

        //    var initCode = string.Format("{0};", memberInitCode);
            
        //    codeBuilder.AddCode(initCode);
        //}

        //public override void AddPublicMemberAndAssignToParent(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        //{
        //    var memberInitCode = PublicOneLineInitCode();

        //    var initCode = string.Format("{0}.{1} = {2};", parentName, ElementName, memberInitCode);
            
        //    codeBuilder.AddCode(initCode);
        //}
    }
}
