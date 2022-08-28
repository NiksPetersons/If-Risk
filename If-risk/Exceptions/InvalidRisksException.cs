using System;

namespace If_risk.Exceptions
{
    public class InvalidRisksException : Exception
    {
        public InvalidRisksException() : base("Invalid risk selection")
        {

        }

        public InvalidRisksException(string message) : base(message)
        {

        }
    }
}