
namespace MockDataVisualizer.ObjectComposite
{
    public class ValueTyeRepresentation : AbstractRepresentation
    {
        public ValueTyeRepresentation(object element, string name) : base(element, name)
        {

        }

        public override string CreateElementInitilizationString(string elementParentExcludingThis, string elementNameFromParent, object parentElement)
        {
            return FormatValue(Element);
        }

        public string FormatValue(object o)
        {
            if(o is string)
                return string.Format("\"{0}\"", o);
            return string.Format("{0}", o);
        }
    }
}
