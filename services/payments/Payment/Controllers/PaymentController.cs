namespace Payment.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase {
        private readonly IPaymentService _paymentService;

        public PaymentController (IPaymentService paymentService) {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult GetAll () {
            return Ok (_paymentService.GetAll ());
        }

        [AllowAnonymous]
        [HttpPost ("register")]
        public async Task<IActionResult> Register ([FromBody] UsersRegistrationViewModel userModel) {
            if (!ModelState.IsValid)
                return BadRequest ("Model is not valid");

            var user = await _paymentService.Register (userModel.Username, userModel.Password);

            if (user == null)
                return BadRequest ("Something went wrong for user registration");

            return Ok (user);
        }

        [AllowAnonymous]
        [HttpPost ("authenticate")]
        public async Task<IActionResult> Authenticate ([FromBody] UsersPaymentViewModel userModel) {
            if (!ModelState.IsValid)
                return BadRequest ("Model is not valid");

            var token = await _paymentService.Authenticate (userModel.Username, userModel.Password);

            if (string.IsNullOrEmpty (token))
                return BadRequest ("Password or username didn't match");

            return Ok (token);
        }
    }
}