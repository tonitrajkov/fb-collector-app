
using FbCollector.Models;

namespace FbCollector.FluentValidators
{
    public static class ConfigureValidators
    {
        public static ValidationFactory Configure()
        {
            var factory = new ValidationFactory();

            //EXAMPLE: Add FluentValidator from User Model
            factory.AddValidator(typeof(PageModelValidator), typeof(PageModel));

            return factory;
        }
    }
}
