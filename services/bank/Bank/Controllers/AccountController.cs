using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Database;
using Bank.Models;
using Bank.Services;
using Bank.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bank.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase {
        private readonly IBankService _bankService;

        public BankController (IBankService bankService) {
            _bankService = bankService;
        }

        [HttpGet]
        public async Task<IActionResult> Get () {
            return Ok (_bankService.GetAll ());
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> Get ([FromRoute] Guid id) {
            var user = _bankService.Get (id);

            if (user == null)
                return NotFound ();

            return Ok (user);
        }

        [HttpGet ("email/{email}")]
        public async Task<ActionResult> Get ([FromRoute] string email) {
            var user = _bankService.Get (email);

            if (user == null)
                return NotFound ();

            return Ok (user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register ([FromBody] UserViewModel userViewModel) {
            if (userViewModel == null)
                return BadRequest ();

            var user = _bankService.Get (_bankService.Register (userViewModel));
            return CreatedAtAction (nameof (Get), new { id = user.UserId }, user);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Update ([FromRoute] Guid id, [FromBody] User user) {
            if (id != user.UserId)
                return BadRequest ();

            _bankService.Update (user);

            return NoContent ();
        }
    }
}