using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FbCollector.Infrastructure.Helpers
{
    public static class ModelStateExtensions
    {
        public static IEnumerable Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                return (IEnumerable)Enumerable.ToList(Enumerable.SelectMany(Enumerable.Where<string>((IEnumerable<string>)modelState.Keys, (Func<string, bool>)(key => Enumerable.Any<ModelError>((IEnumerable<ModelError>)modelState[key].Errors))), (Func<string, IEnumerable<ModelError>>)(key => (IEnumerable<ModelError>)modelState[key].Errors), (key, error) => new
                {
                    Key = key,
                    Message = error.ErrorMessage
                }));
            return (IEnumerable)null;
        }

        public static string ToJson(this object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}
