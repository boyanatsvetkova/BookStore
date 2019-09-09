using BookStore.API.ApiCustomResponse;
using BookStore.API.Filters;
using BookStore.Contracts;
using BookStore.Services;

using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreService _storeService;
        
        public StoreController(StoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost("import")]
        [ServiceFilter(typeof(JsonImporterActionFilter))]
        public IActionResult Import([FromBody]string catalogAsJson)
        {
            IModelError modelError = _storeService.Import(catalogAsJson);
            if (modelError != null)
            {
                // Check the difference between BadRequest and BadRequestObjectResult
                ModelState.AddModelError(modelError.Field, modelError.Message);
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }

            return Ok();
        }

        [HttpGet("quantity/{name}")]
        public ActionResult Quantity(string name)
        {
            _storeService.Quantity(name);

            return Ok();
        }

        [HttpGet("buy")]
        public ActionResult Buy([FromBody]params string[] basketByNames)
        {
            _storeService.Buy(basketByNames);

            return Ok();
        }
    }
}