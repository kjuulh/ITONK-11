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
        private readonly ITraderService _traderService;

        public TobinTaxerController(ITobinTaxerService TobinTaxerService, ITraderService traderService)
        {
            _TobinTaxerService = TobinTaxerService;
            _traderService = traderService;
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
        public async Task<IActionResult> RegisterTaxedTransactions(DateTime dateAdded)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var transaction = await _traderService.GetTransaction(dateAdded);

            if (transaction == null)
                return StatusCode(500);

            var taxedTransaction = _TobinTaxerService.TaxTransaction(transaction);
            
            return CreatedAtAction(nameof(Get), new {id = taxedTransaction.TransactionId}, taxedTransaction);
        }
    }
}