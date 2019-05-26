using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PublicShareControl.Services;
using PublicShareControl.ViewModels;

namespace PublicShareControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;
        private readonly ISharesService _sharesService;

        public PortfolioController(IPortfolioService portfolioService, ISharesService sharesService)
        {
            _portfolioService = portfolioService;
            _sharesService = sharesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_portfolioService.GetAll());
        }

        [HttpGet("{portfolioId}")]
        public async Task<IActionResult> GetByPortfolio([FromRoute] Guid portfolioId) =>
            Ok(await _portfolioService.Get(portfolioId));

        [HttpPost]
        public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var portfolio = await _portfolioService.CreatePortfolio(model.UserId);
            if (portfolio == null)
                return NotFound("User doesn't exist");
            return CreatedAtAction(nameof(GetByPortfolio), new { portfolio.PortfolioId }, portfolio);
        }

        [HttpGet("{portfolioId}/shares/{shareId}")]
        public async Task<IActionResult> GetShareByPortfolio([FromRoute] Guid portfolioId, [FromRoute] Guid shareId) =>
            Ok(await _sharesService.GetByPortfolio(portfolioId, shareId));

        [HttpGet("{portfolioId}/shares")]
        public async Task<IActionResult> GetAllSharesByPortfolio([FromRoute] Guid portfolioId) =>
            Ok(_sharesService.GetAllByPortfolio(portfolioId));

        [HttpPost("{portfolioId}/shares")]
        public async Task<IActionResult> AddShare([FromRoute] Guid portfolioId,
            [FromBody] CreateSharesViewModel sharesViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var share = await _sharesService.Create(portfolioId, sharesViewModel);
            return CreatedAtAction(nameof(GetShareByPortfolio), new { share.Portfolio.PortfolioId, share.ShareId }, share);
        }

        [HttpPut("shares")]
        public async Task<IActionResult> ChangeOwnership([FromBody] ChangeOwnershipViewModel ownershipViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _sharesService.ChangeOwnership(ownershipViewModel);
                return Ok(new { Status = "success" });
            }
            catch (Exception e)
            {
                return BadRequest("Unable to transfer shares, try again with different values");
            }
        }
        
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPortfolioByUser([FromRoute] Guid userId) =>
            Ok(await _portfolioService.GetByUser(userId));
    }
}