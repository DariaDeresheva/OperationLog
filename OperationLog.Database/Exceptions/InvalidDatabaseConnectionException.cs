using System;

namespace OperationLog.Database.Exceptions
{
    public class InvalidDatabaseConnectionException : Exception
    {
        public InvalidDatabaseConnectionException(string message) : base(message)
        {
        }
    }
}
