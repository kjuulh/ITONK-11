using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Users.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        // GET api/health/ping
        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            return Ok("{\"message\": \"pong!\"}");
        }
    }
}