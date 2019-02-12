using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace users.Controllers
{
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            return Ok("Values");
        }
    }
}