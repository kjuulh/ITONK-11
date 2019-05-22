using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTrader.Models;
using StockTrader.Services;
using StockTrader.ViewModels;

namespace StockTrader.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class StockTraderController : ControllerBase {
        private readonly IStockTraderService _stockTraderService;
        private readonly ITransactionsService _transactionsService;

        public StockTraderController (IStockTraderService stockTraderService, ITransactionsService transactionsService) {
            _stockTraderService = stockTraderService;
            _transactionsService = transactionsService;
        }

        [HttpGet]
        public IActionResult Get () {
            var stockTraders = _stockTraderService.GetAll ().ToList ();

            if (stockTraders.Count == 0)
                return Ok (new List<Models.StockTrader> ());

            foreach (var stockTrader in stockTraders) {
                var transactions = _transactionsService.GetAll (stockTrader.StockTraderId).ToList ();
                stockTrader.Transactions = transactions;
            }

            return Ok (stockTraders);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> Get ([FromRoute] Guid id) {
            var stockTrader = await _stockTraderService.Get (id);

            if (stockTrader == null)
                return NotFound ();

            return Ok (stockTrader);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create () {
            var stockTrader = await _stockTraderService.Get (_stockTraderService.Create ());
            return CreatedAtAction (nameof (Get), new { id = stockTrader.StockTraderId }, stockTrader);
        }

        [HttpPut ("{id}")]
        public IActionResult Update ([FromRoute] Guid id, [FromBody] Models.StockTrader stockTrader) {
            if (id != stockTrader.StockTraderId)
                return BadRequest ();

            if (!ModelState.IsValid)
                return BadRequest ();

            _stockTraderService.Update (stockTrader);

            return NoContent ();
        }

        [HttpGet ("{stockTraderId}/transactions")]
        public ActionResult<List<Transaction>> GetTransactions ([FromRoute] Guid stockTraderId) {
            if (!ModelState.IsValid)
                return BadRequest ();

            var transactions = _transactionsService.GetAll (stockTraderId).ToList ();
            foreach (var transaction in transactions) transaction.StockTrader.Transactions = null;
            if (transactions == null)
                return NotFound ();

            return transactions;
        }

        [HttpGet ("transactions/{transactionId}")]
        public async Task<IActionResult> GetTransaction ([FromRoute] Guid transactionId) {
            if (!ModelState.IsValid)
                return BadRequest ();

            var transaction = await _transactionsService.Get (transactionId);

            if (transaction == null)
                return NotFound ();

            return Ok (transaction);
        }

        [HttpPost ("{stockTraderId}/transactions")]
        public IActionResult AddTransaction ([FromRoute] Guid stockTraderId, [FromBody] TransactionCreateViewModel transactionCreateViewModel) {
            if (!ModelState.IsValid)
                return BadRequest ();

            try {
                var transactionId =
                    _transactionsService.AppendTransaction (stockTraderId, transactionCreateViewModel.Amount);
                return Ok (transactionId);
            } catch (Exception e) {
                Console.WriteLine (e);
                throw;
            }
        }

        [HttpPut ("{stockTraderId}/transactions/{transactionId}")]
        public async Task<IActionResult> RevertTransaction ([FromRoute] Guid stockTraderId, [FromRoute] Guid transactionId) {
            if (!ModelState.IsValid)
                return BadRequest ();

            try {
                var revertedTransactionId = await _transactionsService.RevertTransaction (stockTraderId, transactionId);
                return Ok (revertedTransactionId);
            } catch (Exception e) {
                Console.WriteLine (e);
                throw;
            }
        }
    }
}