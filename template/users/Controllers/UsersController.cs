using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace users.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetValues()
        {
            return Ok("Values");
        }
    }
}