using System;
using System.Collections.Generic;
using System.Text;

namespace MockDataDebugVisualizer
{
    public class CodeBuilder
    {
        private readonly StringBuilder _codeBuilder;
        public List<string> CodeList { get; set; }
        //private int _indentLevel;

        public CodeBuilder()
        {
            _codeBuilder = new StringBuilder();
            CodeList = new List<string>();
        }

        public void AddCode(string code)
        {
            //var line = string.Format("{0}{1}", Indentation(indentLevel), code);
            
            CodeList.Add(code);
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
