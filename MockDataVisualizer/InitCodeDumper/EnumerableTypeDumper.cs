using System.Collections;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class EnumerableTypeDumper : AbstractComplexTypeDumper
    {
        public EnumerableTypeDumper(DumperBase parent, object element, string name) : base(parent, element, name)
        {
            ElementName = string.Format("{0}_{1}", name, ObjectCounter++);
        }

        public override void ResolveMembers(CodeBuilder codeBuilder)
        {
            var enumerableElement = Element as IEnumerable;

            foreach (var element in enumerableElement)
            {
                var rep = GetDumper(this, element, element.GetType().Name);

                var oneLineRep = rep as IOneLineInitDumper;
                
                if (oneLineRep != null)
                {
                    var publicInitCode = oneLineRep.PublicOneLineInitCode();

                    var initCode = string.Format("{0}.Add({1});", ElementName, publicInitCode);    

                    codeBuilder.AddCode(initCode);
                }

                var objectDumper = rep as AbstractComplexTypeDumper;
                
                if(objectDumper != null)
                {
                    objectDumper.AddPublicMember(codeBuilder);

                    var initCode = string.Format("{0}.Add({1});", ElementName, rep.ElementName);

                    codeBuilder.AddCode(initCode);
                }
            }
        }

        public override void ResolveTypeInitilization(CodeBuilder codeBuilder)
        {
            var genericArguments = Element.GetType().GetGenericArguments();

            var typeName = Element.GetType().Name;

            if (genericArguments.Length == 1)
            {
                var genericArgument = Element.GetType().GetGenericArguments()[0].Name;

                var initCode = string.Format("var {0} = new {1}<{2}>();", ElementName,
                    typeName.Substring(0, typeName.Length - 2), genericArgument);

                codeBuilder.AddCode(initCode);
            }
        }

        public override void AddPublicMemberAndAssignToParent(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            ResolveTypeInitilization(codeBuilder);

            ResolveMembers(codeBuilder);

            var initCode = string.Format("{0}.{1} = {2};", parentName, elementNameInParent, ElementName);

            codeBuilder.AddCode(initCode);
        }

        public override void AddPrivateMemberAndAssignToParrent(CodeBuilder codeBuilder, string parentName, string elementNameInParent)
        {
            ResolveTypeInitilization(codeBuilder);

            ResolveMembers(codeBuilder);

            var line = string.Format("SetValue({0}, \"{1}\", {2});", parentName, elementNameInParent, ElementName);

            codeBuilder.AddCode(line);
        }
    }
}
