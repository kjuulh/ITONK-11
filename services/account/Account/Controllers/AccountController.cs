using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Database;
using Account.Models;
using Account.Services;
using Account.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Account.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly IAccountService _accountService;

        public AccountController (IAccountService accountService) {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Get () {
            return Ok (_accountService.GetAll ());
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> Get ([FromRoute] Guid id) {
            var user = _accountService.Get (id);

            if (user == null)
                return NotFound ();

            return Ok (user);
        }

        [HttpGet ("email/{email}")]
        public async Task<ActionResult> Get ([FromRoute] string email) {
            var user = _accountService.Get (email);

            if (user == null)
                return NotFound ();

            return Ok (user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register ([FromBody] UserViewModel userViewModel) {
            if (userViewModel == null)
                return BadRequest ();

            var user = _accountService.Get (_accountService.Register (userViewModel));
            return CreatedAtAction (nameof (Get), new { id = user.UserId }, user);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Update ([FromRoute] Guid id, [FromBody] User user) {
            if (id != user.UserId)
                return BadRequest ();

            _accountService.Update (user);

            return NoContent ();
        }
    }
}