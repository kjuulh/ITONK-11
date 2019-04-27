using System.Threading.Tasks;
using Authentication.Services;
using Authentication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_authenticationService.GetAll());
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsersRegistrationViewModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");

            var user = await _authenticationService.Register(userModel.Username, userModel.Password);

            if (user == null)
                return BadRequest("Something went wrong for user registration");

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UsersAuthenticationViewModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");

            var token = await _authenticationService.Authenticate(userModel.Username, userModel.Password);

            if (string.IsNullOrEmpty(token))
                return BadRequest("Password or username didn't match");

            return Ok(token);
        }
    }
}