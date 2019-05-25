using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockTraderBroker.Database;
using StockTraderBroker.Models;
using StockTraderBroker.Services;
using StockTraderBroker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StockTraderBroker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTraderBrokerController : ControllerBase
    {
        private readonly IStockTraderBrokerService _stockTraderBrokerService;

        public StockTraderBrokerController(IAccountService accountService, IStockTraderBrokerService stockTraderBrokerService)
        {
            _stockTraderBrokerService = stockTraderBrokerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _stockTraderBrokerService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var user = await _stockTraderBrokerService.GetUser(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AccountViewModel accountViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _stockTraderBrokerService.CreateAccount(accountViewModel.UserId);
            
            return CreatedAtAction(nameof(Get), new {id = user.UserId}, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] User user)
        {
            if (id != user.UserId)
                return BadRequest();

            await _stockTraderBrokerService.Update(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != Guid.Empty || id == null)
                return BadRequest();

            await _stockTraderBrokerService.Delete(id);

            return NoContent();
        }
    }
}