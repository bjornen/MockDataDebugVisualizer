using System;
using System.Reflection;

namespace MockDataVisualizer.ObjectComposite
{
    public class ObjectRepresentation : AbstractRepresentation
    {
        internal ObjectRepresentation(object element, string name) : base(element, name)
        {
        }

        public static string Create(object o)
        {
            if (o == null) return null;

            var name = o.GetType().Name.ToLower();

            var rep = new ObjectRepresentation(o, name);

            return string.Format("var {0} = {1}", name, rep.CreateElementInitilizationString(string.Empty, name, null));
        }

        public override string CreateElementInitilizationString(string elementParentExcludingThis, string elementNameFromParent, object parentElement)
        {
            var elementName = string.IsNullOrEmpty(elementNameFromParent) ? Element.GetType().Name.ToLower() : elementNameFromParent;

            var elementParentIncludingThis = elementName; 

            if (!string.IsNullOrEmpty(elementParentExcludingThis))
            {
                elementParentIncludingThis = string.Format("{0}.{1}", elementParentExcludingThis, elementName);
            }

            var initilizationString = string.Format("{0};{1}", ElementTypeInitilizationString(), Environment.NewLine);

            foreach (var member in Members)
            {
                //var memberName = string.Format("{0}_{1}", member.Name, ObjectCounter);
                var memberValue = GetMemberValue(member);

                if(memberValue == null) continue;

                var representation = GetRepresentation(memberValue, member.Name);

                string initilization = string.Empty;

                if (representation is ObjectRepresentation)
                {
                    //string memberInit = representation.CreateElementInitilizationString(elementParentIncludingThis, member.Name);
                    //initilization = string.Format("{0}.{1} = {2}", elementParentIncludingThis, member.Name, memberInit);

                    string memberInit = representation.CreateElementInitilizationString(string.Empty, ElementName, Element);
                    initilization = string.Format("var {0}{1} = {2}", ElementName, ObjectCounter, memberInit);
                    initilization = string.Format("{0}{1}.{2} = {3}", initilization, elementParentIncludingThis, ElementName, ElementName);
                }

                if (representation is ValueTyeRepresentation)
                {
                    if (!string.IsNullOrEmpty(elementParentExcludingThis))
                    {
                        initilization = string.Format("{0}.", elementParentExcludingThis);
                    }
                    
                    if (IsMemberPublic(member))
                    {
                        if (CanWriteToMember(member))
                        {
                            string memberInit = representation.CreateElementInitilizationString(elementParentIncludingThis, ElementName, Element);
                            initilization = string.Format("{0}{1}.{2} = {3};", initilization, elementName, ElementName, memberInit);
                        }
                    }
                    else
                    {
                        string memberInit = representation.CreateElementInitilizationString(elementParentIncludingThis, ElementName, Element);
                        initilization = string.Format("{0}{1}.SetValue(\"{2}\", {3});", initilization, elementName, ElementName, memberInit);
                    }
                }

                if (representation is EnumerableRepresentation)
                {
                    string memberInit = representation.CreateElementInitilizationString(string.Empty, ElementName, Element);
                    initilization = string.Format("var {0} = {1}", ElementName, memberInit);
                    initilization = string.Format("{0}{1}.{2} = {3}", initilization, elementParentIncludingThis, ElementName, ElementName);
                }

                initilizationString = string.Format("{0}{1}{2}", initilizationString, initilization, Environment.NewLine);
            }

            return initilizationString;
        }

        //public new string ElementName { get { return base.ElementName; } }

        private string ElementTypeInitilizationString()
        {
            return string.Format("new {0}()", Element.GetType().Name);
        }
    }
}
