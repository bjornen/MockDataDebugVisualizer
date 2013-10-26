using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class DictionaryTypeDumper : Dumper
    {
        public DictionaryTypeDumper(Dumper parent, object element, string name) : base(parent, element, name)
        {
            ElementName = string.Format("{0}_{1}", name, ObjectCounter++);
        }

        public override void GetPublicInitCode(CodeBuilder codeBuilder)
        {
            var genericArguments = Type.GetGenericArguments();

            //var initCode = string.Empty;

            var enumerableElement = Element as IEnumerable;

            if (genericArguments.Length == 2)
            {
                var initCode = string.Format("var {0} = new {1}<{2}, {3}>();", ElementName, TypeName.Substring(0, TypeName.Length - 2), Type.GetGenericArguments()[0].Name, Type.GetGenericArguments()[1].Name);
                codeBuilder.AddCode(initCode);
            }

            foreach (var element in enumerableElement)
            {
                dynamic keyValue = element;

                var keyName = string.Format("{0}_{1}", keyValue.Key.GetType().Name, ObjectCounter++);
                var valueName = string.Format("{0}_{1}", keyValue.Value.GetType().Name, ObjectCounter++);
                
                var keyRep = GetDumper(this, keyValue.Key, keyName);
                var valueRep = GetDumper(this, keyValue.Value, valueName);

                var keyInitCode = keyRep.GetPublicInitCode();
                var valueInitCode = valueRep.GetPublicInitCode();
                
                if (!(keyRep is ValueTypeDumper) && !(keyRep is StringTypeDumper))
                {
                    //initCode = string.Format("{0}{1}{2}", initCode, Environment.NewLine, keyInitCode);
                    codeBuilder.AddCode(keyInitCode);
                }
                if (!(valueRep is ValueTypeDumper) && !(valueRep is StringTypeDumper))
                {
                    //initCode = string.Format("{0}{1}{2}", initCode, Environment.NewLine, valueInitCode);
                    codeBuilder.AddCode(valueInitCode);
                }

                string line;

                if (keyRep is ValueTypeDumper || keyRep is StringTypeDumper)
                {
                    line = string.Format("{0}.Add({1},", ElementName, keyInitCode);
                }
                else
                {
                    line = string.Format("{0}.Add({1},", ElementName, keyRep.ElementName);
                }

                if (valueRep is ValueTypeDumper || valueRep is StringTypeDumper)
                {
                    line = string.Format("{0} {1});", line, valueInitCode);
                }
                else
                {
                    line = string.Format("{0} {1});", line, valueRep.ElementName);
                }

                codeBuilder.AddCode(line);
            }

            //return string.Format("{0}{1}", Environment.NewLine, initCode);
        }

        public override void GetPrivateInitCode(CodeBuilder codeBuilder)
        {
            throw new NotImplementedException();
        }

        public override void AddPublic(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            GetPublicInitCode(codeBuilder);

            var line = string.Format("{0}.{1} = {2};", parentName, elementNameInParent, ElementName);

            codeBuilder.AddCode(line);
        }

        public override void AddPrivate(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            var line = string.Format("SetValue({0}, \"{1}\", {2});", parentName, elementNameInParent, ElementName);

            codeBuilder.AddCode(line);
        }
    }
}
