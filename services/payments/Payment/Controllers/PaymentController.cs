using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment.ViewModels;

namespace Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> DoTransaction([FromBody] CreateTransactionViewModel transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");

            return Ok(transaction);
        }

        [AllowAnonymous]
        [HttpPost("revert")]
        public async Task<IActionResult> RevertTransaction([FromBody] RevertTransactionViewModel transaction)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");

            return Ok(transaction);
        }
    }
}