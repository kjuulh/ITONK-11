using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockBuyer.Services;
using StockBuyer.ViewModels;

namespace StockBuyer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerService _buyerService;

        public BuyerController(IBuyerService buyerService)
        {
            _buyerService = buyerService;
        }

        [HttpPost("request/{requestId}")]
        public async Task<IActionResult> BuyStock([FromRoute] Guid requestId, [FromBody] BuyStockViewModel buyStock)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _buyerService.BuyStock(requestId, buyStock.UserId);
                return Ok(new { Status = "success" });
            }
            catch (System.Exception)
            {
                return BadRequest(new { Status = "failure" });
            }
        }

    }
}