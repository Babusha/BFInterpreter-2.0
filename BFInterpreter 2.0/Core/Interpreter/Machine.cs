using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFInterpreter_2._0.Core.Code;
using BFInterpreter_2._0.Core.Tape;

namespace BFInterpreter_2._0.Core.Interpreter
{
    public class Machine : IMachine
    {
        public ITape Tape { get; set; }
        public ICode Code { get; set; }
        public IInputOutput InputOutput { get; set; }

        public Machine(IInputOutput inputOutput, ICode code)
        {
            throw new NotImplementedException();
        }
        public void Inc()
        {
            throw new NotImplementedException();
        }

        public void Dec()
        {
            throw new NotImplementedException();
        }

        public void Next()
        {
            throw new NotImplementedException();
        }

        public void Prev()
        {
            throw new NotImplementedException();
        }
    }
}
