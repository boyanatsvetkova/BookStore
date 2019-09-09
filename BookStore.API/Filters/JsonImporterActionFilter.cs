using BookStore.API.ApiCustomResponse;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore.API.Filters
{
    // Check if the attribute keyword is added at the end of the class the attribute will be registred automatically
    public class JsonImporterActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault();
            if (param.Value == null)
            {
                context.ModelState.AddModelError("JSONCatalog", "null");
                context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(context.ModelState));
                return;
            }

            JObject bookStore = JObject.Parse(param.Value.ToString());

            var bundleAssembly = AppDomain.CurrentDomain.GetAssemblies()
                            .First(x => x.FullName.Contains("BookStore.API"));
            var assemblyPath = new Uri(bundleAssembly.CodeBase).LocalPath;
            var jsonPath = Path.Combine(Path.GetDirectoryName(assemblyPath), "Documents", "BookStoreJSONSchema.json"));

            JSchema bookStoreSchema = JSchema.Parse(File.ReadAllText(jsonPath));

            IList<string> messages;
            bool isSchemaValid = bookStore.IsValid(bookStoreSchema, out messages);

            if (!isSchemaValid)
            {
                context.ModelState.AddModelError("JSONCatalog", messages.First());
                context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(context.ModelState));
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }
    }
}
