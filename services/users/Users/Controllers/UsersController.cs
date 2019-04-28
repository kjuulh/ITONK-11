using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Users.Database;
using Users.Models;
using Users.Services;
using Users.ViewModels;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usersService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            var user = _usersService.Get(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        public ActionResult Get([FromRoute] string email)
        {
            var user = _usersService.Get(email);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userId = await _usersService.Register(userViewModel);
            var user = await _usersService.Get(userId);

            if (user == null)
                return StatusCode(500);
            
            return CreatedAtAction(nameof(Get), new {id = user.UserId}, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] User user)
        {
            if (id != user.UserId)
                return BadRequest();

            await _usersService.Update(user);

            return NoContent();
        }
    }
}