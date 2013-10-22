
namespace MockDataVisualizerTestConsole.Tests.TestObjects
{
    public class EnumObject
    {
        public Colors Colors1 { get; set; }
        private ColorsWithNumbers ColorsWithNumbers { get; set; }
    }

    public enum Colors
    {
        yellow,
        blue,
        red
    }

    public enum ColorsWithNumbers
    {
        yellow = 3,
        blue = 44,
        red = 99
    }
}
