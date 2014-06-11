using System;
using System.Collections.Generic;

namespace MockDataVisualizerTestConsole.Tests.ComplexTypeDumpersTests.TestObjects
{
    [Serializable]
    public class Root
    {
        private string _backing;
        private string _privateField;

        private string PrivateProperty { get; set; }

        public int RootId { get; set; }
        public string RootName { get; set; }
        public IFoo Foo { get; set; }
        public Foo Foo2 { get; set; }
        private Bar PrivateBar { get; set; }
        public IEnumerable<Foo> Foos { get; set; }
        public IEnumerable<IFoo> Ifoos { get; set; }
        private List<IFoo> FooList { get; set; }
        public IList<Foo> IfooList { get; set; }
        public List<int> Numbers { get; set; }
        public Foo[] FooArray { get; set; }
        public int[] NumbersArray { get; set; }
        public Dictionary<string, string> Dictionary { get; set; }
        public Root Self { get; set; }

        public Foo ProtectedSetFoo { get; protected set; }

        public string Backing
        {
            get { return _backing; } 
            //private set { _pin = value; }
        }

        public void InitializePrivates()
        {
            _backing = "The private backing field";
            _privateField = "The private field";
            PrivateProperty = "The private property";
        }

        public void InitilizeProtected()
        {
            ProtectedSetFoo = new Foo {FooValue = "Protected Foo"};
        }
    }
}
