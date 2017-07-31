using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Utills.PMSExceptions
{
    public class PMSException : Exception
    {
        public PMSException() { }

        public PMSException(string message) : base(message) { }

        public PMSException(string message, Exception innerException) : base(message, innerException) { }
    }
}
