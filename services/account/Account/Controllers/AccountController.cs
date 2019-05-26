using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Models;
using Account.Services;
using Account.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionsService _transactionsService;

        public AccountController(IAccountService accountService, ITransactionsService transactionsService)
        {
            _accountService = accountService;
            _transactionsService = transactionsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var accounts = _accountService.GetAll().ToList();

            if (accounts.Count == 0)
                return Ok(new List<Models.Account>());

            foreach (var account in accounts)
            {
                var transactions = _transactionsService.GetAll(account.AccountId).ToList();
                account.Transactions = transactions;
            }

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var account = await _accountService.Get(id);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            var account = await _accountService.Get(await _accountService.Create());
            return CreatedAtAction(nameof(Get), new {id = account.AccountId}, account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] Models.Account account)
        {
            if (id != account.AccountId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            await _accountService.Update(account);

            return NoContent();
        }

        [HttpGet("{accountId}/transactions")]
        public ActionResult<List<Transaction>> GetTransactions([FromRoute] Guid accountId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var transactions = _transactionsService.GetAll(accountId).ToList();
            foreach (var transaction in transactions) transaction.Account.Transactions = null;

            return transactions;
        }

        [HttpGet("transactions/{transactionId}")]
        public async Task<IActionResult> GetTransaction([FromRoute] Guid transactionId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var transaction = await _transactionsService.Get(transactionId);

            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost("{accountId}/transactions")]
        public async Task<IActionResult> AddTransaction([FromRoute] Guid accountId,
            [FromBody] TransactionCreateViewModel transactionCreateViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var transactionId =
                    await _transactionsService.AppendTransaction(accountId, transactionCreateViewModel.Amount);
                return Ok(transactionId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPut("{accountId}/transactions/{transactionId}")]
        public async Task<IActionResult> RevertTransaction([FromRoute] Guid accountId, [FromRoute] Guid transactionId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var revertedTransactionId = await _transactionsService.RevertTransaction(accountId, transactionId);
                return Ok(revertedTransactionId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}