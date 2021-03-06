﻿using System;
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

namespace Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;

        public BankController(IAccountService accountService, IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bankService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var user = await _bankService.GetUser(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] AccountViewModel accountViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _bankService.CreateAccount(accountViewModel.UserId);
            
            return CreatedAtAction(nameof(Get), new {id = user.UserId}, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] User user)
        {
            if (id != user.UserId)
                return BadRequest();

            await _bankService.Update(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id != Guid.Empty || id == null)
                return BadRequest();

            await _bankService.Delete(id);

            return NoContent();
        }
    }
}