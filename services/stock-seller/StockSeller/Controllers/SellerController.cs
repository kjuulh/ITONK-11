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

        [HttpPost()]
        public async Task<IActionResult> SellShare([FromBody] SellShareViewModel sellShare)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var request = await _sellerService.SellShare(sellShare.UserId,
                    sellShare.ShareId,
                    sellShare.Amount);
                if (request == null)
                    return BadRequest("Something went wrong. Try again later");
                return Ok(request);
            }
            catch (System.Exception)
            {
                return BadRequest(new { Status = "failure" });
            }
        }

    }
}