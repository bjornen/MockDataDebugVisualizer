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

        public override string GetPublicInitCode()
        {
            var genericArguments = Type.GetGenericArguments();

            var initCode = string.Empty;

            var enumerableElement = Element as IEnumerable;

            if (genericArguments.Length == 2)
            {
                initCode = string.Format("var {0} = new {1}<{2}, {3}>();", ElementName, TypeName.Substring(0, TypeName.Length - 2), Type.GetGenericArguments()[0].Name, Type.GetGenericArguments()[1].Name);
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
                    initCode = string.Format("{0}{1}{2}", initCode, Environment.NewLine, keyInitCode);
                }
                if (!(valueRep is ValueTypeDumper) && !(valueRep is StringTypeDumper))
                {
                    initCode = string.Format("{0}{1}{2}", initCode, Environment.NewLine, valueInitCode);
                }
                
                if (keyRep is ValueTypeDumper || keyRep is StringTypeDumper)
                {
                    initCode = string.Format("{0}{1}{2}.Add({3},", initCode, Environment.NewLine, ElementName, keyInitCode);
                }
                else
                {
                    initCode = string.Format("{0}{1}{2}.Add({3},", initCode, Environment.NewLine, ElementName, keyRep.ElementName);
                }

                if (valueRep is ValueTypeDumper || valueRep is StringTypeDumper)
                {
                    initCode = string.Format("{0} {1});", initCode, valueInitCode);
                }
                else
                {
                    initCode = string.Format("{0} {1});", initCode, valueRep.ElementName);
                }
            }

            return string.Format("{0}{1}", Environment.NewLine, initCode);
        }

        public override string GetPrivateInitCode()
        {
            throw new NotImplementedException();
        }

        public override string AddPublic(string initCode, string parentName, string elementNameInParent)
        {
            return string.Format("{0}{1}{2}{3}{4}.{5} = {6};", initCode, Environment.NewLine, GetPublicInitCode(), Environment.NewLine, parentName, elementNameInParent, ElementName);
        }

        public override string AddPrivate(string initCode, string parentName, string elementNameInParent)
        {
            return string.Format("{0}{1}{2}{3}SetValue({4}, \"{5}\", {6});", initCode, Environment.NewLine, GetPublicInitCode(), Environment.NewLine, parentName, elementNameInParent, ElementName);
        }
    }
}
