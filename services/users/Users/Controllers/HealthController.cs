using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Users.Controllers
{
  [Route("api/[controller]")]
  public class HealthController : Controller
  {
    public HealthController() { }

    // GET api/health
    [HttpGet("")]
    public ActionResult<IEnumerable<string>> Gets()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/health/5
    [HttpGet("{id}")]
    public ActionResult<string> GetById(int id)
    {
      return "value" + id;
    }

    // POST api/health
    [HttpPost("")]
    public void Post([FromBody] string value) { }

    // PUT api/health/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value) { }

    // DELETE api/health/5
    [HttpDelete("{id}")]
    public void DeleteById(int id) { }
  }
}