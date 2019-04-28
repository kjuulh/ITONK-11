using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        // GET api/health/ping
        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            return Ok(new {Message = "pong!"});
        }
    }
}