using System;
using System.Web.Mvc;

namespace FbCollector.Infrastructure.Helpers
{
    [Serializable]
    public class InvalidModelStateException : Exception
    {
        public InvalidModelStateException(ModelStateDictionary modelState)
            : base(ModelStateExtensions.ToJson((object)new
            {
                Errors = ModelStateExtensions.Errors(modelState)
            }))
        {
        }
    }
}
