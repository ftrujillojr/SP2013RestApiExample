using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP2013Example
{
    class SP2013Exception: System.Exception
    {
        public SP2013Exception()
        {
        }

        public SP2013Exception(string message)
            : base(message)
        {
        }

        public SP2013Exception(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

