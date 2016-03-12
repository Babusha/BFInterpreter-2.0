using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFInterpreter_2._0.Core.Exceptions
{
    public class StackEmptyException: Exception
    {
        public StackEmptyException()
        {
        }

        public StackEmptyException(string message)
            : base(message)
        {
        }

        public StackEmptyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
