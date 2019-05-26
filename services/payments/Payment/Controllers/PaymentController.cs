using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payment.Services;
using Payment.ViewModels;

namespace Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> DoTransaction([FromBody] CreateTransactionViewModel transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");

            try
            {
                if (await _paymentService.CreateTransaction(transaction))
                    return Accepted();
                return NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Something went wrong, try again in a minute");
            }
        }

        [AllowAnonymous]
        [HttpPost("revert")]
        public async Task<IActionResult> RevertTransaction([FromBody] RevertTransactionViewModel transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");
            
            try
            {
                if (await _paymentService.RevertTransaction(transaction))
                    return Accepted();
                return NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Something went wrong, try again in a minute");
            }
        }
    }
}