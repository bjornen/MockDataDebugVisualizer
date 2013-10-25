using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockDataDebugVisualizer.InitCodeDumper
{
    public class CodeBuilder
    {
        private readonly StringBuilder _codeBuilder;
        private readonly List<string> _codeInitList;

        public CodeBuilder()
        {
            _codeBuilder = new StringBuilder();
            _codeInitList = new List<string>();
        }

        public void AddCode(string code)
        {
            _codeInitList.Add(code);
        }

        public void AddCode(List<string> codeInitList)
        {
            _codeInitList.AddRange(codeInitList);
        }

        public string Indentation(int level)
        {
            return new string('\t', level);
        }
    }
}
