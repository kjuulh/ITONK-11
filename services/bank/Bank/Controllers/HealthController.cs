using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Controllers {
    [Route ("api/[controller]")]
    public class HealthController : Controller {
        // GET api/health/ping
        [HttpGet ("ping")]
        public async Task<ActionResult<string>> Ping () {
            return Ok ("{\"message\": \"pong!\"}");
        }
    }
}