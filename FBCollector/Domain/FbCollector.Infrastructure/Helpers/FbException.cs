using System;

namespace FbCollector.Infrastructure.Helpers
{
    public class FbException : Exception
    {
        public FbException(string message)
            : base(message)
        {
        }
    }
}
