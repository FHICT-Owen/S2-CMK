using System;
using System.Collections.Generic;
using System.Text;

namespace eTransport_Utility.Exceptions
{
    public class LogicException : Exception
    {
        public LogicException(string message) : base(message)
        { }
    }
}
