using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TobinTaxer.Services;

namespace TobinTaxer.Controllers
{
    [Route("api/taxer")]
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


        [HttpGet("current")]
        public async Task<IActionResult> RegisterTaxedTransactions()
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var transaction = await _traderService.GetTransactions(DateTime.Now.Year, DateTime.Now.Month);

            if (transaction == null)
                return StatusCode(500);

            var taxedTransaction = await _TobinTaxerService.TaxTransaction(transaction);

            return Ok(taxedTransaction);
        }

        [HttpGet("{year}/{month}")]
        public async Task<IActionResult> RegisterTaxedTransactions([FromRoute] int year, [FromRoute] int month)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var transaction = await _traderService.GetTransactions(DateTime.Now.Year, DateTime.Now.Month);

            if (transaction == null)
                return NotFound();

            var taxedTransaction = await _TobinTaxerService.TaxTransaction(transaction);

            return Ok(taxedTransaction);
        }
    }
}