using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class ValueTypeDumper : Dumper
    {
        public ValueTypeDumper(Dumper parent, object element, string name) : base(parent, element, name){}

        public override string GetPublicInitCodeDump()
        {
            var value = string.Format("{0}", Convert.ToString(Element).ToLower());
            
            if(value.Contains(","))
                value = string.Format("{0}", value.Replace(',', '.'));

            if (Element is uint)
                value = string.Format("{0}U", value);

            if (Element is ulong)
                value = string.Format("{0}UL", value);
            
            if (Element is float)
                value = string.Format("{0}F", value);

            if (Element is double)
                value = string.Format("{0}D", value);

            if (Element is decimal)
                value = string.Format("{0}M", value);

            return value;
        }

        public override string GetPrivateInitCodeDump()
        {
            return string.Format("SetValue({0}, \"{1}\", {2})", Parent.ElementName, ElementName, GetPublicInitCodeDump());
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
