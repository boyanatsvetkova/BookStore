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
        public IActionResult Import([FromBody]string catalogAsJson)
        {
            _storeService.Import(catalogAsJson);           

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