using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Database;
using Authentication.Models;
using Authentication.Services;
using Authentication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController (IAuthenticationService authenticationService) {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get () {
            return Ok (_authenticationService.GetAll ());
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> Get ([FromRoute] Guid id) {
            var user = _authenticationService.Get (id);

            if (user == null)
                return NotFound ();

            return Ok (user);
        }

        [HttpGet ("email/{email}")]
        public async Task<ActionResult> Get ([FromRoute] string email) {
            var user = _authenticationService.Get (email);

            if (user == null)
                return NotFound ();

            return Ok (user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register ([FromBody] UserViewModel userViewModel) {
            if (userViewModel == null)
                return BadRequest ();

            var user = _authenticationService.Get (_authenticationService.Register (userViewModel));
            return CreatedAtAction (nameof (Get), new { id = user.UserId }, user);
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Update ([FromRoute] Guid id, [FromBody] User user) {
            if (id != user.UserId)
                return BadRequest ();

            _authenticationService.Update (user);

            return NoContent ();
        }
    }
}