using FluentValidation.Results;
using Microsoft.AspNet.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.Contract.Extensions
{
    public static class ModelStateExtensions
    {
        public static void ApplyValidationFailures(this ModelStateDictionary state, IList<ValidationFailure> validationFailures)
        {
            foreach(var failure in validationFailures)
            {
                state.AddModelError(failure.PropertyName, failure.ErrorMessage);
            }
        }
    }
}
