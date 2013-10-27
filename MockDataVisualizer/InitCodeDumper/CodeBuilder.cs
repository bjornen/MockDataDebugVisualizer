using System;
using System.Collections.Generic;
using System.Text;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class CodeBuilder
    {
        private List<string> CodeList { get; set; }
        private Stack<string> CodeStack { get; set; } 

        public CodeBuilder()
        {
            CodeList = new List<string>();
            CodeStack = new Stack<string>();
        }

        public void AddCode(string code)
        {
            CodeList.Add(code);
        }

        public void PushCode(string code)
        {
            CodeStack.Push(code);
        }

        public string PopCode()
        {
            return CodeStack.Pop();
        }

        public void AddCode(CodeBuilder builder)
        {
            CodeList.AddRange(builder.CodeList);
        }

        public string Indentation(int level)
        {
            return new string('\t', level);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var line in CodeList)
            {
                sb.Append(string.Format("{0}{1}", line.Trim(), Environment.NewLine));
            }
            return sb.ToString().Trim(Environment.NewLine.ToCharArray());
        }
    }
}
