using System;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class ValueTypeDumper : Dumper, IOneLineInit
    {
        public ValueTypeDumper(Dumper parent, object element, string name) : base(parent, element, name){}

        public override void AddPrivate(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var privateInitCode = PrivateOneLineInitCode();
            
            var initCode = string.Format("{0};", privateInitCode);
            
            codeBuilder.AddCode(initCode);
        }

        public override void AddPublic(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var memberInitCode = PublicOneLineInitCode();
            
            var initCode = string.Format("{0}.{1} = {2};", parentName, ElementName, memberInitCode);
            
            codeBuilder.AddCode(initCode);
        }

        public string PublicOneLineInitCode()
        {
            var value = string.Format("{0}", Convert.ToString(Element).ToLower());

            if (value.Contains(","))
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

        public string PrivateOneLineInitCode()
        {
            return string.Format("SetValue({0}, \"{1}\", {2})", Parent.ElementName, ElementName, PublicOneLineInitCode());
        }
    }
}
