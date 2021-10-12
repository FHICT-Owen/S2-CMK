using System;
using System.Collections.Generic;
using System.Text;

namespace eTransport_Utility.Exceptions
{
    public class DalSQLException : Exception
    {
        public DalSQLException(string message) : base(message)
        { }
    }
}
