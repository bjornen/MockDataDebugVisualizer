using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class DateTimeTypeDumper : Dumper
    {
        public DateTimeTypeDumper(Dumper parent, object element, string name) : base(parent, element, name) {}

        public override void GetPublicInitCode(CodeBuilder codeBuilder)
        {
            var dt = Element is DateTime ? (DateTime) Element : new DateTime();

            return string.Format("new DateTime({0})", dt.Ticks);
        }

        public override string GetPrivateInitCode()
        {
            return string.Format("SetValue({0}, \"{1}\", {2})", Parent.ElementName, ElementName, GetPublicInitCode(TODO));
        }

        public override string AddPrivate(string initCode, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPrivateInitCode();
            initCode = string.Format("{0}{1}{2};", initCode, Environment.NewLine, memberInitCode);
            return initCode;
        }

        public override string AddPublic(string initCode, string parentName, string elementNameInParent)
        {
            var memberInitCode = GetPublicInitCode(TODO);
            initCode = string.Format("{0}{1}{2}.{3} = {4};", initCode, Environment.NewLine, parentName, ElementName, memberInitCode);
            return initCode;
        }
    }
}
