using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PublicShareControl.Models;
using PublicShareControl.Services;

namespace PublicShareControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;


        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_portfolioService.GetAll());
        }
        
        [HttpPost]
        public async Task<IActionResult> CreatePortfolio([FromBody] PortfolioModel portfolioModel)
        {

            //if (!ModelState.IsValid)
            //{
              //  return BadRequest();
            //}
            /*
            if (_sharesService.GetByName(shareViewModel.Name.ToLower())!=null)
            {
                return BadRequest("A share with that name already exists");
            }*/
            var portfolio = _portfolioService.Get(_portfolioService.CreatePortfolio(portfolioModel));
            return CreatedAtAction(nameof(Get), portfolio.Id, portfolio);
        }
        
    }
}