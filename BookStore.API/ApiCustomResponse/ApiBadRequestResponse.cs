using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.API.ApiCustomResponse
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public ApiBadRequestResponse(ModelStateDictionary modelState) 
            : base(400, "Validation failed")
        {
            Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)));
        }

        public IEnumerable<ValidationError> Errors { get; }
    }
}
