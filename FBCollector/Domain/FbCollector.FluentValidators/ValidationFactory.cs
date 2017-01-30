using System;
using System.Collections.Generic;
using FluentValidation;

namespace FbCollector.FluentValidators
{
    public class ValidationFactory : ValidatorFactoryBase
    {
        private readonly Dictionary<Type, object> _validators = new Dictionary<Type, object>();

        public override IValidator CreateInstance(Type validatorType)
        {
            try
            {
                var item = _validators[validatorType];

                return item as IValidator;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Add new validator in _validator Dictionary
        /// </summary>
        /// <param name="validatorType">Validator Type</param>
        /// <param name="modelType">Model Type</param>
        public void AddValidator(Type validatorType, Type modelType)
        {
            var generic = typeof(IValidator<>);
            Type[] args = { modelType };
            Type key = generic.MakeGenericType(args);
            object instance = Activator.CreateInstance(validatorType);
            _validators.Add(key, instance);
        }
    }
}
