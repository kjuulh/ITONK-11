using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockTraderBroker.Database;
using StockTraderBroker.Models;
using StockTraderBroker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTraderBroker.ViewModels;

namespace StockTraderBroker.Controllers
{
    [Route("api/Trader")]
    [ApiController]
    public class StockTraderBrokerController : ControllerBase
    {
        private readonly ITraderService _traderService;
        public StockTraderBrokerController(ITraderService traderService)
        {
            this._traderService = traderService;
        }

        [HttpPost("sell")]
        public async Task<IActionResult> SellShare([FromBody] SellShareViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var request = await _traderService.CreateRequest(viewModel);

            return CreatedAtAction(nameof(GetRequestById), new { request.RequestId }, request);
        }

        [HttpPut("sell/{requestId}")]
        public async Task<IActionResult> UpdateRequest([FromRoute] Guid requestId, [FromBody] UpdateRequestViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var request = await _traderService.UpdateRequest(requestId, viewModel);

                if (request == null)
                    return NotFound("request not found");
                return Ok(request);
            }
            catch (System.Exception)
            {
                return BadRequest("Couldn't update model");
                throw;
            }
        }

        [HttpPost("buy/{requestId}")]
        public async Task<IActionResult> BuyShare([FromRoute] Guid requestId, [FromBody] BuyShareViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var share = await _traderService.BuyShareAsync(requestId, viewModel);

            if (share == false)
                return NotFound("request was not found, or user has insufficient funds");

            return Ok(new {Status = "success"});
        }

        [HttpGet("{requestId}")]
        public async Task<IActionResult> GetRequestById([FromRoute] Guid requestId)
        {
            Request request = null;

            try
            {
                request = await _traderService.GetRequestByIdAsync(requestId);

                if (request == null)
                    return NotFound("Request not found");

                return Ok(request);
            }
            catch (System.Exception)
            {
                return BadRequest("Couldn't return user");
            }
        }

        [HttpDelete("{requestId}")]
        public async Task<IActionResult> DeleteRequestById([FromRoute] Guid requestId)
        {

            try
            {
                await _traderService.DeleteRequest(requestId);
                return Ok(new { Status = "success" });
            }
            catch (System.Exception)
            {
                return NotFound("request not found");
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_traderService.GetAll().ToEnumerable());
    }
}