
using System.Linq;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public abstract class AbstractComplexType : DumperBase
    {
        protected AbstractComplexType(object element, string name) : base(element, name)
        {
            var typeName = name;

            if (IsGenericType(element.GetType()))
            {
                typeName = ResolveTypeName(element.GetType());
            }

            ElementName = string.Format("{0}_{1}", LowerCaseFirst(typeName), ObjectCounter++);
        }

        public abstract void ResolveTypeInitilization(CodeBuilder codeBuilder);
        public abstract void ResolveMembers(CodeBuilder codeBuilder);

        public override void ResolveInitCode(CodeBuilder codeBuilder)
        {
            if (IsElementAlreadyTouched())
            {
                codeBuilder.PushInitValue(GetNameOfAlreadyTouchedElement());
            }
            else
            {
                AddFoundElement();

                ResolveTypeInitilization(codeBuilder);

                ResolveMembers(codeBuilder);

                codeBuilder.PushInitValue(ElementName);                
            }
        }

        private static string LowerCaseFirst(string variableName)
        {
            return string.IsNullOrEmpty(variableName) ? string.Empty : char.ToLower(variableName[0]) + variableName.Substring(1);
        }

        private bool IsElementAlreadyTouched()
        {
            var hash = Element.GetHashCode();

            return _foundElements.Any(t => _foundElements.ContainsKey(hash));
        }

        private string GetNameOfAlreadyTouchedElement()
        {
            var hash = Element.GetHashCode();

            if (_foundElements.ContainsKey(hash))
            {
                return _foundElements[hash];
            }

            return null;
        }

        private void AddFoundElement()
        {
            var hash = Element.GetHashCode();

            if (!_foundElements.ContainsKey(hash))
            {
                _foundElements.Add(hash, ElementName);
            }
        }
    }
}
