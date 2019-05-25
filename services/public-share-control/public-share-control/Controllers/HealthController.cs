using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PublicShareControl.Controllers
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