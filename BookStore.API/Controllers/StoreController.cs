using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore.Core;
using BookStore.Services.Contracts;
using Microsoft.AspNetCore.Http;
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
            _storeService.Import(catalogAsJson);

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
                return BadRequest(new { ExceptionMessage = nex.Message, Books = nex.Missing });
            }         
        }
    }
}