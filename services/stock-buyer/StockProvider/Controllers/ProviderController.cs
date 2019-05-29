using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stockbuyer.Services;

namespace Stockbuyer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class buyerController : ControllerBase
    {
        private readonly IbuyerService _buyerService;

        public buyerController(IbuyerService buyerService)
        {
            _buyerService = buyerService;
        }
    }
}