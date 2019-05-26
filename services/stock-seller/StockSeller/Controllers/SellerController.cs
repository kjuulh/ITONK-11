using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockSeller.Services;
using StockSeller.ViewModels;

namespace StockSeller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpPost("request/{requestId}")]
        public async Task<IActionResult> BuyStock([FromRoute] Guid requestId, [FromBody] BuyStockViewModel buyStock)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _sellerService.BuyStock(requestId, buyStock.UserId);
                return Ok(new { Status = "success" });
            }
            catch (System.Exception)
            {
                return BadRequest(new { Status = "failure" });
            }
        }

    }
}