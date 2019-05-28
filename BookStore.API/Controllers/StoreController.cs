using BookStore.Core.Extensions;
using BookStore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStore _storeService;
        
        public StoreController(IStore storeService)
        {
            _storeService = storeService;
        }

        [HttpPost("import")]
        public ActionResult Import([FromBody]string catalogAsJson)
        {
            string errorMessage;
            _storeService.Import(catalogAsJson, out errorMessage);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest(errorMessage);
            }

            return NoContent();
        }

        [HttpGet("quantity/{name}")]
        public ActionResult Quantity(string name)
        {
            bool isAvailable = _storeService.IsBookAvailable(name);

            if (!isAvailable)
            {
                return NotFound(name);
            }

            int quantityAvailableOfBook = _storeService.Quantity(name);

            return Ok(quantityAvailableOfBook);
        }

        [HttpGet("buy")]
        public ActionResult Buy([FromBody]params string[] basketNames)
        {
            try
            {
                double basketPrice = _storeService.Buy(basketNames);

                return Ok(basketPrice);
            }
            catch (NotEnoughInventoryException nex)
            {
                return BadRequest(new { Exception = nex.Message, Books = nex.Missing });
            }         
        }
    }
}