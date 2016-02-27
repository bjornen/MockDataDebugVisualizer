using System;
using System.Collections.Generic;
using System.Text;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class CodeBuilder
    {
        private List<string> CodeList { get; set; }
        private Stack<string> InitValueStack { get; set; } 

        public CodeBuilder()
        {
            CodeList = new List<string>();
            InitValueStack = new Stack<string>();
        }

        public void AddCode(string code)
        {
            CodeList.Add(code);
        }

        public void PushInitValue(string code)
        {
            InitValueStack.Push(code);
        }

        public string PopInitValue()
        {
            return InitValueStack.Pop();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var line in CodeList)
            {
                sb.Append($"{line.Trim()}{Environment.NewLine}");
            }

            return sb.ToString().Trim(Environment.NewLine.ToCharArray());
        }
    }
}
