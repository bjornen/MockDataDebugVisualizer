
namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class Statement
    {
        public string ParentName { get; set; }
        public string MemberName { get; set; }
        public string Value { get; set; }
        public bool IsPublic { get; set; }

        public static Statement Creat(string parent, string memberName, string value, bool isPublic)
        {
            if (value == null) return null;

            var statement = new Statement
                            {
                                ParentName = parent,
                                MemberName = memberName,
                                Value = value,
                                IsPublic = isPublic
                            };

            return statement;
        }
    }
}
