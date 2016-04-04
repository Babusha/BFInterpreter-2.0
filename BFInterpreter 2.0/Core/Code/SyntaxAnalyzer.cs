using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFInterpreter_2._0.Core.File;

namespace BFInterpreter_2._0.Core.Code
{
    public class SyntaxAnalyzer : ISyntaxAnalyzer
    {
        public char[] Code { get; set; }
        public SyntaxAnalyzer(string programText)
        {
            Code = programText.ToCharArray();
            Clean();
        }

        private void Clean()
        {
            var cleaned = new List<char>();
            foreach (var item in Code)
            {
                if (item != '+' ||
                    item != '-' ||
                    item != '>' ||
                    item != '<' ||
                    item != '.' ||
                    item != ',' ||
                    item != '[' ||
                    item != ']')
                {
                    continue;
                }
                else
                {
                    cleaned.Add(item);
                }
            }

            Code = cleaned.ToArray();
        }

    }
}
