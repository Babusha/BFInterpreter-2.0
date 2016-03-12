using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFInterpreter_2._0.Core.Exceptions
{
    public class InvalidFileExtensionException : Exception
    {
        public InvalidFileExtensionException()
        {
        }

        public InvalidFileExtensionException(string message)
            : base(message)
        {
        }

        public InvalidFileExtensionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
