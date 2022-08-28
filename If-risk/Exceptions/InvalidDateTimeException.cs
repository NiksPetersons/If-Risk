using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace If_risk.Exceptions
{
    public class InvalidDateTimeException : Exception
    {
        public InvalidDateTimeException() 
            : base ("Invalid dates")
        {
            
        }

        public InvalidDateTimeException(string message)
            : base(message)
        {
         
        }
    }
}
