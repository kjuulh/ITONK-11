using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockProvider.Services;
using StockProvider.ViewModels;

namespace StockProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShare([FromBody] CreateShareViewModel createShare)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var share = await _providerService.CreateShare(createShare);
                return Ok(share);
            }
            catch (System.Exception)
            {
                return BadRequest("Couldn't create share");
            }
        }
    }
}