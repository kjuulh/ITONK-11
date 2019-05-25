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
    public class TraderController : ControllerBase
    {
        private readonly ITraderService _traderService;
        public TraderController(ITraderService traderService)
        {
            this._traderService = traderService;
        }
        
        [HttpGet]
        public IActionResult GetAll() => Ok(_traderService.GetAll().ToEnumerable());
        
        [HttpGet("closed")]
        public IActionResult GetClosed() => Ok(_traderService.GetAll().Where(r => r.Status == "closed").ToEnumerable());
        
        [HttpGet("open")]
        public IActionResult GetOpen() => Ok(_traderService.GetAll().Where(r => r.Status == "open").ToEnumerable());

        [HttpGet("closed/{year}/{month}")]
        public IActionResult GetClosedByMonth([FromRoute] int year, [FromRoute] int month)
        {
            var requests = _traderService.GetAll();
            var closedRequests = requests.Where(r => r.DateClosed.Year == year && r.DateClosed.Month == month && r.Status == "closed");
            return Ok(closedRequests.ToEnumerable());
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

    }
}