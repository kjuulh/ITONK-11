using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TobinTaxer.Models;
using TobinTaxer.Services;
using TobinTaxer.ViewModels;

namespace TobinTaxer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TobinTaxerController : ControllerBase
    {
        private readonly ITobinTaxerService _TobinTaxerService;

        public TobinTaxerController(ITobinTaxerService TobinTaxerService)
        {
            _TobinTaxerService = TobinTaxerService;
        }


        //Get all transactions
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_TobinTaxerService.GetAll());
        }


        //Get transaction from id
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            var transaction = _TobinTaxerService.Get(id);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] TransactionViewModel transactionViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var transactionId = await _TobinTaxerService.Register(transactionViewModel);
            var transaction = await _TobinTaxerService.Get(transactionId);

            if (transaction == null)
                return StatusCode(500);
            
            return CreatedAtAction(nameof(Get), new {id = transaction.TransactionId}, transaction);
        }
    }
}