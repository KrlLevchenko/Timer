using System;

namespace Timer.ExceptionHandling.Exceptions
{
    public class FileNotFoundException: Exception
    {
        public FileNotFoundException(string fileId): base($"File {fileId} not found")
        {
            
        }
    }
}