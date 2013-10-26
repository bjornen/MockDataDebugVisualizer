using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class DateTimeTypeDumper : Dumper, IOneLineInit
    {
        public DateTimeTypeDumper(Dumper parent, object element, string name) : base(parent, element, name) {}

        public string PublicOneLineInitCode()
        {
            var dt = Element is DateTime ? (DateTime) Element : new DateTime();

            return string.Format("new DateTime({0})", dt.Ticks);
        }

        public string PrivateOneLineInitCode()
        {
            var publicInitCode = PublicOneLineInitCode();

            return string.Format("SetValue({0}, \"{1}\", {2})", Parent.ElementName, ElementName, publicInitCode);
        }

        public override void AddPrivate(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var memberInitCode = PrivateOneLineInitCode();

            var initCode = string.Format("{0};", memberInitCode);
            
            codeBuilder.AddCode(initCode);
        }

        public override void AddPublic(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var memberInitCode = PublicOneLineInitCode();

            var initCode = string.Format("{0}.{1} = {2};", parentName, ElementName, memberInitCode);
            
            codeBuilder.AddCode(initCode);
        }
    }
}
