using System;

namespace MockDataVisualizerTestConsole.Tests.TestObjects
{
    public interface IFoo
    {
        string FooValue { get; set; }
    }

    [Serializable]
    public class Foo : IFoo
    {
        private int _privateFooField;
        private string PrivateFooProperty { get; set; }
        public string FooValue { get; set; }
        public Bar Bar { get; set; }
        public Bar Bar2 { get; set; }
        public Root Root { get; set; }
    }
}
