using System;

namespace Timer.ExceptionHandling.Exceptions
{
    public class DirectoryNotFoundException: Exception
    {
        public DirectoryNotFoundException(string directoryId): base($"Directory {directoryId} not found")
        {
            
        }
    }
}