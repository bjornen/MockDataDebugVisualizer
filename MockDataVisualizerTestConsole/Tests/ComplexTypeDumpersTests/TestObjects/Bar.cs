using System;

namespace MockDataVisualizerTestConsole.Tests.ComplexTypeDumpersTests.TestObjects
{
    [Serializable]
    public class Bar
    {
        public string BarValue { get; set; }
        public Foo BarFoo { get; set; }
    }
}
