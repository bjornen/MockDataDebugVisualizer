using System;
using System.Collections;

namespace MockDataVisualizer.ObjectComposite
{
    public class EnumerableRepresentation : AbstractRepresentation
    {
        public EnumerableRepresentation(object element, string name) : base(element, name)
        {

        }

        public override string CreateElementInitilizationString(string elementParentExcludingThis, string elementNameFromParent, object parentElement)
        {
            var elementName = string.IsNullOrEmpty(elementNameFromParent) ? Element.GetType().Name.ToLower() : elementNameFromParent;

            var elementParentIncludingThis = elementName;

            if (!string.IsNullOrEmpty(elementParentExcludingThis))
            {
                elementParentIncludingThis = string.Format("{0}.{1}", elementParentExcludingThis, elementName);
            }

            var type = Element.GetType();

            var genericArgument = type.GetGenericArguments()[0].Name;

            var initStr = string.Format("new {0}<{1}>();{2}", type.Name.Substring(0, type.Name.Length - 2), genericArgument, Environment.NewLine);

            var enumerableElement = Element as IEnumerable;

            foreach (var o in enumerableElement)
            {
                var representation = GetRepresentation(o, o.GetType().Name.ToLower());

                var enumerableElementInitilizationString = representation.CreateElementInitilizationString(string.Empty, ElementName, Element);

                initStr = string.Format("{0}var {1} = {2}{3}", initStr, ElementName, enumerableElementInitilizationString, Environment.NewLine);

                initStr = string.Format("{0}{1}.Add({2});{3}", initStr, elementParentIncludingThis, ElementName, Environment.NewLine);
            }
            
            return initStr;
        }
    }
}
