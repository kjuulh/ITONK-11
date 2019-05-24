using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shares.Services;
using Shares.ViewModels;

namespace Shares.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharesController : ControllerBase
    {
        private readonly ISharesService _sharesService;

        public SharesController(ISharesService sharesService)
        {
            _sharesService = sharesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var shares = await Task.Run(() => _sharesService.GetAll());
            return Ok(shares);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get([FromRoute] string name)
        {
            var share = await Task.Run(() => _sharesService.GetByName(name));
            return Ok(share);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var share = await Task.Run(() => _sharesService.Get(id));

            if (share == null)
                return NotFound();

            return Ok(share);
        }


        [HttpPost]
        public async Task<IActionResult> EstablishShare([FromBody] ShareViewModel shareViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (_sharesService.GetByName(shareViewModel.Name.ToLower()) != null)
                return BadRequest("A share with that name already exists");

            var share = _sharesService.Get(await _sharesService.Establish(shareViewModel));

            return CreatedAtAction(nameof(Get), share.ShareId, share);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ChangeShareViewModel share)
        {
            if (id != share.ShareId)
                return BadRequest();

            await _sharesService.Update(share);

            return NoContent();
        }
    }
}